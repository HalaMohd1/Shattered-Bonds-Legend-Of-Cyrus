using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyController
{
    public float HorizontalSpeed;  // Speed at which the enemy moves horizontally
    public float VerticalSpeed;    // Speed of the vertical "wobble"
    public float amplitude;        // Amplitude of the vertical movement (wobble effect)
    private Vector3 temp_position; // Temporary position to apply movement
    public float moveSpeed;        // Speed of horizontal movement
    private PlayerController player;

    // Add references for pointA and pointB
    public Transform pointA;       // Starting point
    public Transform pointB;       // End point
    private bool movingRight = true; // Direction flag for flipping (true = moving right, false = moving left)

    // Start is called before the first frame update
    void Start()
    {
        temp_position = transform.position;  // Initialize the enemy's position
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any additional behavior or checks here if necessary
    }

    void FixedUpdate()
    {
        // Horizontal movement (move the enemy left or right)
        temp_position.x += (movingRight ? HorizontalSpeed : -HorizontalSpeed) * Time.deltaTime;

        // Apply vertical wobble (using a sine wave for smooth up/down movement)
        // Keeping the vertical position tied to the y-value of pointA (or pointB)
        float verticalWobble = Mathf.Sin(Time.realtimeSinceStartup * VerticalSpeed) * amplitude;
        temp_position.y = pointA.position.y + verticalWobble; // Keep the vertical position near pointA's y-value, with wobble

        // Apply the updated position to the enemy
        transform.position = temp_position;

        // Flip the enemy when it reaches the start or end points
        if (movingRight && temp_position.x >= pointB.position.x)
        {
            movingRight = false; // Change direction to the left
            Flip(); // Flip the enemy horizontally
        }
        else if (!movingRight && temp_position.x <= pointA.position.x)
        {
            movingRight = true;  // Change direction to the right
            Flip(); // Flip the enemy horizontally
        }
    }

    // Flip the enemy horizontally based on its movement direction
    public new void Flip() // Use 'new' to hide the inherited Flip method
    {
        isFacingRight = !isFacingRight;  // Toggle facing direction
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Flip horizontally
    }

    // OnTriggerEnter2D: Trigger for damaging the player when in contact
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
            Flip();  // Flip horizontally when colliding with the player
        }
    }
}
