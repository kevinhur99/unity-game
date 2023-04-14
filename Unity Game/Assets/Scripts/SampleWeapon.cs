using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleWeapon : Weapon
{
    public override void Fire(Vector2 attackOrigin)
    {
        if (CanFire())
        {
            if (HitTarget(null)) // Null parameter to indicate that the target is the first collider hit by the raycast
            {
                Debug.Log("Pistol hits target!");
            }
            else
            {
                Debug.Log("Pistol misses target.");
            }
            ResetFireTime();
        }
    }
}