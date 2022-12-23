using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePart : MonoBehaviour
{
    private bool _isDestroyed;

    private void OnCollisionEnter(Collision collision)
    {
        if(_isDestroyed) return;
        var rb = gameObject.AddComponent<Rigidbody>();
        var normal = collision.contacts[0].normal;
        rb.AddForce(normal * 7, ForceMode.Impulse);
        //rb.mass = 100;
        _isDestroyed = true;
    }
}
