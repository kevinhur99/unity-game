
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _lookInput;
    private bool _isController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Update the velocity of the player using the user input
        _rigidbody.velocity = _movementInput * _speed;


        // Update the look direction of the player using the user input
        float angle;
        if (_isController)
        {
            angle = Vector2.SignedAngle(Vector2.up, _lookInput);

            // Prevent the look direction from snapping back to neutral when controller joystick is neutral
            if (angle == 0f)
            {
                angle = _rigidbody.rotation;
            }
        } else
        {
            // Determine location of the mouse and the angle of the player
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(_lookInput);
            Vector2 direction = mousePosition - new Vector3(_rigidbody.position.x, _rigidbody.position.y, 0);
            angle = Vector2.SignedAngle(Vector2.up, direction);
        }
        _rigidbody.rotation = angle;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
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