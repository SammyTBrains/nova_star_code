using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosses : MonoBehaviour
{
    [SerializeField]
    private GameObject _initText, _nextText, _otherBoss;
    [SerializeField]
    private GameObject _model, parent;
    [SerializeField]
    private GameObject _youDidItTimeline;
    [SerializeField]
    private AudioSource _bgMusic;

    private Animator _anim;
    private bool isHit = false;
    private Player _player;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is null in Bosses!");
        }
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is null in Bosses");
        }
    }

    public void ChangePattern()
    {
        isHit = !isHit;
        _anim.SetBool("Hit", isHit);
    }

    public void InitTextInactive()
    {
        if (_initText != null)
        {
            _initText.SetActive(false);
        }
    }

    public void NextTextActive()
    {
        Destroy(_model, 3.0f);
        if (_nextText != null)
        {
            StartCoroutine(WaitForExplosion());
        }
        else if (_nextText == null && _youDidItTimeline != null)
        {
            gameObject.tag = "None";
            _player.gameObject.tag = "None";
            StartCoroutine(WaitForExplosionWin());
        }
    }

    IEnumerator WaitForExplosionWin()
    {
        yield return new WaitForSeconds(6.0f);
        if(_bgMusic != null)
        {
            _bgMusic.Stop();
        }
        _youDidItTimeline.SetActive(true);
    }

    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(6.0f);
        Destroy(this.gameObject);
        if (_otherBoss == null)
        {
            _nextText.SetActive(true);
            parent.SetActive(false);
        }
    }
}
