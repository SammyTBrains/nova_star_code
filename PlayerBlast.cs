using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlast : MonoBehaviour
{
    [SerializeField]
    private float _speed = 15.0f;
    [SerializeField]
    private float _damageVal;
    [SerializeField]
    private GameObject _hitMark;

    private Player _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player in Player Blast is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        if (transform.position.x >= 24)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(_damageVal);              
                GameObject mark = Instantiate(_hitMark, other.ClosestPoint(transform.position) , Quaternion.identity);
                _player.DestroyHitMark(mark);
                Destroy(this.gameObject);
            }
        }
    }
}
