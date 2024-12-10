using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
   public float moveSpeed;  
    public float moveRange = 5f;  // How far the platform moves in each direction
    private Vector3 startingPosition;     // Starting position of the platform
    private bool movingRight = true;  // Direction the platform is moving

    void Start()
    {
        // Store the starting position
        startingPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = startingPosition.y;  // Keep Y position fixed
        transform.position = currentPosition;
        
        // Move the platform left and right
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);  // Move right
            if (transform.position.x >= startingPosition.x + moveRange)  // Check if the platform reaches the right limit
            {
                movingRight = false;  // Change direction
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);  // Move left
            if (transform.position.x <= startingPosition.x - moveRange)  // Check if the platform reaches the left limit
            {
                movingRight = true;  // Change direction
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {Debug.Log("Collision detected with: " + other.gameObject.name);
        // If it hits anything, reverse direction
        if (other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("Platform")) 
        {
            movingRight = !movingRight;  // Reverse direction
        }
    }
}


