using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Thruster")]
    [SerializeField]
    private Transform _thruster;
    [SerializeField]
    private Animator _thrusterAnim;

    [Header("Attributes")]
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _lightFlash, _lightFlash2;
    [SerializeField]
    private GameObject _explosion, _model, _shield;
    [SerializeField]
    private GameObject _hitMark;

    [Header("Player Weapon")]
    [SerializeField]
    private int _currentWeapon;
    [SerializeField]
    private GameObject[] _weapons;

    [Header("Audio")]
    [SerializeField]
    private AudioSource _weaponSound;
    [SerializeField]
    private AudioSource _playerHitSound;
    [SerializeField]
    private AudioSource _shieldHitSound;
    [SerializeField]
    private AudioSource _shipHitShip;
    [SerializeField]
    private AudioSource _bgMUsic;

    [Header("Timelines")]
    [SerializeField]
    private GameObject _gameOverTimeline;
    [SerializeField]
    private GameObject _enemyWaveTimeline;

    private Animator _anim;
    private bool _isShielded = false;
    private bool _weaponFired = false;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-18, 0, 20);
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.Log("Animator is null in player");
        }

        _currentWeapon = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }

    void Movement()
    {
        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(HorizontalInput, VerticalInput, 0) * _speed * Time.deltaTime);

        Vector3 transformPos = transform.position;
        transformPos.x = Mathf.Clamp(transformPos.x, -19, 19);
        transformPos.y = Mathf.Clamp(transformPos.y, -12, 12);
        transform.position = transformPos;
        if (Mathf.Abs(HorizontalInput) > 0 || Mathf.Abs(VerticalInput) > 0)
        {
            _anim.SetFloat("Idle", 1);
        }
        else
        {
            _anim.SetFloat("Idle", 0);
        }
        if (VerticalInput > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 10);
        }
        else if (VerticalInput < 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, -10);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        _thrusterAnim.SetFloat("Idle", Mathf.Abs(HorizontalInput));
        _thrusterAnim.SetFloat("Move", HorizontalInput);
    }

    void Shoot()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, Vector3.right, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.transform.gameObject.tag == "Enemy")
            {
                if (_weaponFired == false)
                {
                    _weaponFired = true;
                    StartCoroutine(InstantiateCooldown());
                }
            }
        }
    }

    IEnumerator InstantiateCooldown()
    {
        Instantiate(_weapons[_currentWeapon - 1], transform.position, Quaternion.identity);
        _weaponSound.Play();
        yield return new WaitForSeconds(0.5f);
        _weaponFired = false;
    }

    public void PlayerHit()
    {
        if (_isShielded == false)
        {
            _currentWeapon--;
            _playerHitSound.Play();
            if (_currentWeapon > 0)
            {
                UIManager.Instance.CurrentWeaponUpdate(_currentWeapon);
            }
            if (_currentWeapon <= 0)
            {
                _bgMUsic.Stop();
                GameObject expln = Instantiate(_explosion, transform.position, Quaternion.identity);
                StartCoroutine(SetModelInActive());
                Destroy(expln, 3.5f);
                StartCoroutine(GameOverRoutine());
            }
        }
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(3f);
        _enemyWaveTimeline.SetActive(false);
        _gameOverTimeline.SetActive(true);
    }

    public void ActivateShield()
    {
        _isShielded = true;
        _shield.SetActive(true);
        StartCoroutine(ShieldTimeRoutine());
    }

    IEnumerator ShieldTimeRoutine()
    {
        yield return new WaitForSeconds(11f);
        _isShielded = false;
        _shield.SetActive(false);
    }

    IEnumerator SetModelInActive()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.tag = "None";
        _model.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Upgrade" && _currentWeapon < 3)
        {
            _currentWeapon++;
            UIManager.Instance.CurrentWeaponUpdate(_currentWeapon);
            _lightFlash.SetActive(true);
            _lightFlash2.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(StartFlashRoutine());
        }

        if (other.tag == "Enemy")
        {
            GameObject mark = Instantiate(_hitMark, other.ClosestPoint(transform.position), Quaternion.identity);
            _shipHitShip.Play();
            DestroyHitMark(mark);
        }
    }

    IEnumerator StartFlashRoutine()
    {
        yield return new WaitForSeconds(1f);
        _lightFlash.SetActive(false);
        _lightFlash2.SetActive(false);
    }

    public void DestroyHitMark(GameObject hitMark)
    {
        Destroy(hitMark, 0.3f);
    }

    public void IncreaseScore()
    {
        _score++;
        UIManager.Instance.ScoreUpdate(_score);
    }

    public void ShieldSound()
    {
        _shieldHitSound.Play();
    }
}
