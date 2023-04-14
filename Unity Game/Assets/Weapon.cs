using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float fireRate = 0.5f;
    public int damage = 10;

    // The time when the weapon can fire again
    private float nextFireTime; 

    // This method will be implemented by the concrete weapon classes
    public abstract void Fire(); 

    protected bool CanFire()
    {
        return Time.time >= nextFireTime;
    }

    protected void ResetFireTime()
    {
        nextFireTime = Time.time + fireRate;
    }

    protected bool HitTarget(Collider2D hitBox)
    {
        // TODO: Implement later:
    }
}
