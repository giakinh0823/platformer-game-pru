using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameController gameController;
    [SerializeField]
    public Transform respawnPoint;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    public Sprite passive;
    [SerializeField]
    public Sprite active;

    private Collider2D collider2D;
    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameController.UpdateCheckpoint(respawnPoint.position);
            spriteRenderer.sprite = active;
            collider2D.enabled = false;
        }

    }
}
