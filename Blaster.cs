using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Player is null in blaster");
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
        if(transform.position.x <= -24)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _player.PlayerHit();            
            Destroy(this.gameObject);
        }

        if(other.tag == "PlayerShield")
        {
            _player.ShieldSound();
            Destroy(this.gameObject);
        }
    }
}
