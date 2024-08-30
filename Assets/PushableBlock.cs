using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    public float pushForce = 5f; // Force applied when the player pushes the block

    private Rigidbody2D rb;
    private bool isBeingPushed = false;
    private Transform playerTransform;
    private Vector2 offset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If the block is being pushed, make it follow the player's movement
        if (isBeingPushed && playerTransform != null)
        {
            // Keep the block at the same relative position to the player
            rb.MovePosition((Vector2)playerTransform.position + offset);
        }
        else
        {
            // Stop the block completely when not being pushed
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Reset the pushing flag for the next frame
        isBeingPushed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is colliding with the block
        if (collision.gameObject.CompareTag("Player"))
        {
            // Store the player transform and calculate the offset between player and block
            playerTransform = collision.transform;
            offset = rb.position - (Vector2)playerTransform.position;

            // Mark the block as being pushed
            isBeingPushed = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Continue pushing if the player is still in contact with the block
        if (collision.gameObject.CompareTag("Player"))
        {
            isBeingPushed = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Detach the block from the player when the player stops pushing
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }
}
