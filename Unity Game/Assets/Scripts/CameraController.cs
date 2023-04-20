using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Camera playerCamera;

    Vector3 refVelocity;
    float smoothTime = 0.2f, zStart;

    // Variables determining the maximum distance between camera and player
    float cameraDistanceX = 7f;
    float cameraDistanceY = 3.5f;
    float maxCameraOffsetX = 0.95f;
    float maxCameraOffsetY = 0.9f;

    // Variables for the screen shake
    Vector3 shakeOffset;
    float shakeMagnitude, shakeTimeEnd;
    bool shaking;

    // Start is called before the first frame update
    void Start()
    {
        zStart = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateShake();
        UpdateCameraPosition();
    }

    private void UpdateShake()
    {
        if (!shaking || Time.time > shakeTimeEnd)
        {
            shaking = false;
            shakeOffset = Vector3.zero;
        } else
        {
            shakeOffset *= shakeMagnitude;
        }
    }

    /// <summary>
    /// Obtains the position of the cursor relative to the center of the screen represented as 
    /// a vector. The maximum magnitude of this vector is determined by the max camera offsets.
    /// </summary>
    /// <returns>Vector3 pointing from the center of the screen to the mouse</returns>
    private Vector3 GetMousePosition()
    {
        // Obtain a vector pointing from the center of the screen to the cursor
        Vector2 returnVector = playerCamera.ScreenToViewportPoint(Input.mousePosition) * 2;
        returnVector -= Vector2.one;

        // If the cursor is getting close to the edge of the screen, do not continue pushing
        // the camera
        if (Mathf.Abs(returnVector.x) > maxCameraOffsetX) {
            returnVector.x = (returnVector.x / Mathf.Abs(returnVector.x)) * maxCameraOffsetX;
        }
        if (Mathf.Abs(returnVector.y) > maxCameraOffsetY)
        {
            returnVector.y = (returnVector.y / Mathf.Abs(returnVector.y)) * maxCameraOffsetY;
        }

        return returnVector;
    }

    /// <summary>
    /// Obtains the location for the camera to go.
    /// </summary>
    /// <returns>Vector3 representing the location for the camera to go</returns>
    private Vector3 GetTargetPosition()
    {
        Vector3 mouseOffset = GetMousePosition();

        // Obtain the location where we want the camera to go by adding the mouse position to the
        // player's position
        Vector3 returnVector = player.position + new Vector3(mouseOffset.x * cameraDistanceX, 
            mouseOffset.y * cameraDistanceY, 0);
        returnVector += shakeOffset;
        returnVector.z = zStart;
        
        return returnVector;
    }

    /// <summary>
    /// Smoothly moves the camera towards the cursor based on its distance from the player
    /// </summary>
    private void UpdateCameraPosition()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GetTargetPosition(), 
            ref refVelocity, smoothTime);
    }

    public void Shake(Vector3 direction, float magnitude, float length)
    {
        shaking = true;
        shakeOffset = direction;
        shakeMagnitude = magnitude;
        shakeTimeEnd = Time.time + length;
    }

}
