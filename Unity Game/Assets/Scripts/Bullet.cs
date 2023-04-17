using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Create explosion effect lasting for 0.25 seconds
        GameObject explosionEffect = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosionEffect, 0.25f);

        // Destroy the bullet
        Destroy(gameObject);
    }

}
