using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _health;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject _smokeTrail;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private AudioSource _weaponSound;
    [SerializeField]
    private AudioSource _enemyHitSound;
   
    private GameObject[] _spawnedShield;
    private GameObject[] _spawnedUpgrade;
    private bool _laserFired = false;
    private Bosses _bosses;
    private float _totalHealth;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null in Enemy");
        }
        _bosses = GetComponent<Bosses>();
        if (_bosses != null)
        {
            _bosses.InitTextInactive();
        }
        _totalHealth = _health;
        StartCoroutine(SpawnPowerUps());
    }

    // Update is called once per frame
    void Update()
    {
        _spawnedShield = GameObject.FindGameObjectsWithTag("Shield");
        _spawnedUpgrade = GameObject.FindGameObjectsWithTag("Upgrade");
        Shoot();
    }

    void Shoot()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, Vector3.left, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.transform.gameObject.tag == "Player")
            {
                if (_laserFired == false)
                {
                    _laserFired = true;
                    StartCoroutine(InstantiateCooldown());
                }
            }
        }
    }

    IEnumerator InstantiateCooldown()
    {
        Instantiate(_laser, transform.position, Quaternion.identity);
        _weaponSound.Play();
        yield return new WaitForSeconds(0.5f);
        _laserFired = false;
    }

    IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
            if (_spawnedUpgrade.Length < 1 && _spawnedShield.Length < 1)
            {
                Instantiate(_powerUps[Random.Range(0, 2)], transform.position, Quaternion.identity);
            }
        }
    }

    public void Damage(float hit)
    {
        _health -= hit;
        _enemyHitSound.Play();

        if (_bosses != null)
        {
            _bosses.ChangePattern();
        }

        float healthPercentage = (_health / _totalHealth) * 100;

        if (healthPercentage < 70 && _smokeTrail.activeInHierarchy == false)
        {
            _smokeTrail.SetActive(true);
            _player.IncreaseScore();
        }

        if (_health <= 0)
        {
            _smokeTrail.SetActive(false);
            if (_bosses != null)
            {
                _explosion.SetActive(true);
                _bosses.NextTextActive();
            }
            if (_bosses == null)
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
}
