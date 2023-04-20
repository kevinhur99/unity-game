using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Camera playerCamera;

    public float bulletForce = 20f;

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            // Create and shoot a bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

            // Set camera shake
            playerCamera.GetComponent<CameraController>().Shake(firePoint.up, 1.5f, 0.05f);

            // Automatically destroy bullet after 5 seconds
            Destroy(bullet, 5f);
        }
    }
}
