using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D rigidBody2D;

    [SerializeField]
    public int speed;
    private float speedMultiper;
    private bool btnPress;

    [Range(1, 10)]
    [SerializeField]
    public float accelateration;

    private bool isWallTouch;
    public LayerMask wallerLayerMask;
    public Transform wallCheckPoint;
    private Vector2 relativeTransform;

    public bool isOnPlatform;
    public Rigidbody2D platformRigitbody2d;

    [SerializeField]
    public ParticleController particleController;

    public void Start()
    {
        UpdateRelativeTransform();
    }

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();
        float targetSpeed = speed * speedMultiper * relativeTransform.x;

        if (isOnPlatform)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(targetSpeed + platformRigitbody2d.velocity.x, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(targetSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.03f, 0.5f), 0, wallerLayerMask);
        if (isWallTouch)
        {
            Flip();
        }
    }

     
    public void Flip()
    {
        if(particleController != null)
        {
            particleController.PlayTouchParticle(wallCheckPoint.position);
        }
        transform.Rotate(0, 180, 0);
        UpdateRelativeTransform();
    }

    public void UpdateRelativeTransform()
    {
        relativeTransform = transform.InverseTransformDirection(Vector2.one);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            btnPress = true;
        } else if (value.canceled)
        {
            btnPress = false;
        }
    }

    public void UpdateSpeedMultiplier()
    {
        if(btnPress && speedMultiper < 1)
        {
            speedMultiper += Time.deltaTime * accelateration;
        }else if(!btnPress && speedMultiper > 0)
        {
            speedMultiper -= Time.deltaTime * accelateration;
            if(speedMultiper < 0) {
                speedMultiper = 0;
            }
        }
    }
}
