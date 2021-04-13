using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 5f;

    protected Player _player;

    public virtual void Init()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Player is null in power ups");
        }
    }

    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
        if (transform.position.x <= -24)
        {
            Destroy(this.gameObject);
        }
    }
}
