using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector2 checkPointPos;
    public Rigidbody2D playerRigidBody2D;


    private void Awake()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        checkPointPos = transform.position;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    public IEnumerator Respawn(float duration)
    {
        playerRigidBody2D.simulated = false;
        playerRigidBody2D.velocity = Vector2.zero; 
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(duration);
        transform.position = checkPointPos;
        transform.localScale = Vector3.one;
        playerRigidBody2D.simulated=true;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkPointPos = pos;
    }
}
