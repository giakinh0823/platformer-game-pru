using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] 
    private int speed;
    private float speedMultiper;
    private bool btnPress;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        float  targetSpeed = speed * speedMultiper;
        _rigidbody2D.velocity = new Vector2(targetSpeed, _rigidbody2D.velocity.y);
    }


    public void OnMove(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            btnPress = true;
            speedMultiper = 1;
        } else if (value.canceled)
        {
            btnPress = false;
            speedMultiper = 0;
        }
    }
}
