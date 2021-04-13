using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player.ActivateShield();
            Destroy(this.gameObject);
        }
    }
}