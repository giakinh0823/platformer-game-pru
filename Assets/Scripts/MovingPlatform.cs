using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    public GameObject ways;
    private Transform[] wayPoints;
    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    [SerializeField]
    public float speed;
    private Vector3 targetPos;

    private MovementController movementController;
    private Rigidbody2D rigidbody2d;
    private Vector3 moveDirection;

    private Rigidbody2D playerRigidbody2d;

    private void Awake()
    {
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerRigidbody2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        wayPoints = new Transform[ways.transform.childCount];
        for(int i = 0; i< ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }


    private void Start()
    {
        pointIndex = 0;
        pointCount = wayPoints.Length;
        targetPos= wayPoints[1].transform.position;
        DirectionCaculator(); 
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        transform.position = targetPos;
        if(pointIndex == pointCount - 1)
        {
            direction = -1;
        } 

        if(pointIndex == 0)
        {
            direction = 1;
        }
        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
        DirectionCaculator();
    }

    private void FixedUpdate()
    {
        rigidbody2d.velocity = moveDirection * speed;
    }

    public void DirectionCaculator()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = true;
            movementController.platformRigitbody2d = rigidbody2d;
            playerRigidbody2d.gravityScale = playerRigidbody2d.gravityScale * 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = false;
            playerRigidbody2d.gravityScale = playerRigidbody2d.gravityScale / 50;
        }
    }
}
