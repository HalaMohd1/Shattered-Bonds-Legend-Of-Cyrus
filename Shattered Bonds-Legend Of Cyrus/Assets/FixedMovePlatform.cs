using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMovePlatform : MonoBehaviour
{
    public float speed = 3f; // Speed of the platform's movement
    public Transform startPoint; // Start position of the platform
    public Transform endPoint; // End position of the platform
    private bool movingToEnd = true; // Flag to check which direction the platform is moving in
    private bool isPlayerOnPlatform = false; // To check if the player is on the platform
    private Vector3 targetPosition; // The target position the platform is moving towards

    void Start()
    {
          GetComponent<SpriteRenderer>().enabled = true;
        // Set the platform's initial position
        transform.position = startPoint.position;
        targetPosition = endPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If the platform is moving, move it toward the target position
        if (isPlayerOnPlatform)
        {
            MovePlatform();
        }
    }

    // Move the platform between start and end positions
    void MovePlatform()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            // If the platform reaches the target, switch direction
            if (movingToEnd)
            {
                targetPosition = startPoint.position; // Set the target to the start position
            }
            else
            {
                targetPosition = endPoint.position; // Set the target to the end position
            }
            movingToEnd = !movingToEnd; // Reverse the direction of movement
        }

        // Move the platform toward the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    // Detect when the player steps onto the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = true; // Player is on the platform, start moving it
            collision.transform.SetParent(transform); // Attach the player to the platform
        }
    }

    // Detect when the player exits the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = false; // Player left the platform, stop moving it
            collision.transform.SetParent(null); // Detach the player from the platform
        }
    }
}
