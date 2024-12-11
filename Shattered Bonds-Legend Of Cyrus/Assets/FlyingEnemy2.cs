using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy2 : EnemyController
{
    public float HorizontalSpeed;  // Speed at which the enemy moves horizontally
    public float VerticalSpeed;    // Speed of the vertical "wobble"
    public float amplitude;        // Amplitude of the vertical movement (wobble effect)
    private Vector3 temp_position; // Temporary position to apply movement
    public float attackRange = 2f; // Range to detect the player
    private bool isAttacking = false; // Is the enemy in attacking mode?

    private PlayerController player; // Reference to the player
    private Animator anim;           // Reference to the Animator

    public Transform pointA;         // Starting point
    public Transform pointB;         // End point
    private bool movingRight = true; // Direction flag for flipping (true = moving right, false = moving left)

    // Start is called before the first frame update
    void Start()
    {
        temp_position = transform.position;  // Initialize the enemy's position
        anim = GetComponent<Animator>();     // Get the Animator component
        player = FindObjectOfType<PlayerController>(); // Reference to the player
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance(); // Handle attack range detection
    }

    void FixedUpdate()
    {
        // Horizontal movement
        if (movingRight)
        {
            temp_position.x += HorizontalSpeed * Time.deltaTime;
        }
        else
        {
            temp_position.x -= HorizontalSpeed * Time.deltaTime;
        }

        // Apply vertical wobble
        float verticalWobble = Mathf.Sin(Time.realtimeSinceStartup * VerticalSpeed) * amplitude;
        temp_position.y = pointA.position.y + verticalWobble;

        // Update the enemy's position
        transform.position = temp_position;

        // Flip the enemy when reaching waypoints
        if (movingRight && temp_position.x >= pointB.position.x)
        {
            movingRight = false; // Switch to moving left
            Flip(); // Flip the sprite
        }
        else if (!movingRight && temp_position.x <= pointA.position.x)
        {
            movingRight = true;  // Switch to moving right
            Flip(); // Flip the sprite
        }
    }

    void CheckPlayerDistance()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                anim.SetBool("isAttacking", true); // Trigger attack animation
            }
        }
        else
        {
            if (isAttacking)
            {
                isAttacking = false;
                anim.SetBool("isAttacking", false); // Revert to normal animation
            }
        }
    }

    public new void Flip()
    {
        // Flip the enemy horizontally
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Damage the player
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }

            // Flip direction upon collision
            Flip();
            movingRight = !movingRight; // Change direction after collision
        }
    }
}
