using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aiming : MonoBehaviour
{
    private Vector2 _lookInput;
    private bool _isController;
    public Camera playerCamera;
    public GameObject gunPivot;

    private void FixedUpdate()
    {
        // Update the look direction of the player using the user input
        float angle;
        if (_isController)
        {
            angle = Vector2.SignedAngle(Vector2.right, _lookInput);

            // Prevent the look direction from snapping back to neutral when controller joystick is neutral
            if (angle == 0f)
            {
                return;
            }
        }
        else
        {
            // Determine location of the mouse and the angle of the player
            Vector3 mousePosition = playerCamera.ScreenToWorldPoint(_lookInput);
            Vector2 direction = mousePosition - new Vector3(gunPivot.transform.position.x, gunPivot.transform.position.y, 0);
            angle = Vector2.SignedAngle(Vector2.right, direction);
        }
        gunPivot.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    public void OnControlsChanged(PlayerInput playerInput)
    {
        _isController = playerInput.currentControlScheme.Equals("Gamepad");
    }
}
