using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem movementParticle;
    [SerializeField] 
    private ParticleSystem fallParticle;
    [SerializeField]
    private ParticleSystem touchParticle;

    [Range(0, 10)]
    [SerializeField]
    public int occurAfterVeolocity;

    [Range(0f, 0.2f)]
    [SerializeField]
    public float dustFormationPeriod;

    [SerializeField]
    private Rigidbody2D playerRigidBody2D;

    private float counter;
    private bool isOnGround;

    private void Update()
    {
        counter += Time.deltaTime;
        if(isOnGround && Math.Abs(playerRigidBody2D.velocity.x) > occurAfterVeolocity)
        {
            if( counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;    
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            fallParticle.Play();
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    private void PlayTouchParticle()
    {
        touchParticle.Play();
    }
}
