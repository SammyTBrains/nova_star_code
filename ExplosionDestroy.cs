using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    [SerializeField]
    private AudioSource _explosionSound;

    private void Start()
    {
        _explosionSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 5.0f);
    }
}
