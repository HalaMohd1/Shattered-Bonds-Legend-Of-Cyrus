using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingenemyAttack : EnemyController
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

    // Attack related variables
    public float attackRange = 3f; // Distance at which enemy starts attacking
    private bool isAttacking = false; // To check if the enemy is in attacking mode
    private bool isFacingPlayer = false; // To check if the enemy is facing the player
    private bool isInAttackRange = false; // To check if the enemy is within attack range

    private Animator anim;
    public float attackCooldown ; // Time before transitioning to mouth fully open (optional for delay)
    public float attackTimer = 0f; // Timer to manage transitions

    // Start is called before the first frame update
    void Start()
    {
        temp_position = transform.position;  // Initialize the enemy's position
        anim = GetComponent<Animator>(); // Get the Animator component
        player = FindObjectOfType<PlayerController>(); // Get the player reference
    }

    // Update is called once per frame
    void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < attackRange)
        {
            // Enemy is within attack range, start mouth opening animation
            isInAttackRange = true;
            anim.SetBool("isFacingPlayer", true); // Face the player

            if (!isAttacking) // Only start mouth opening if not already attacking
            {
                isAttacking = true;
                anim.SetBool("isAttacking", true); // Trigger attacking animation (mouth opening)
                attackTimer = attackCooldown; // Start the timer for mouth fully open
            }

            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime; // Countdown before fully opening mouth
            }
            else
            {
                // Transition to mouth fully open when the timer is finished
                anim.SetBool("isAttacking", false); // Stop mouth opening animation
                anim.SetBool("isMouthFullyOpen", true); // Mouth fully open animation
                // Deal damage when the mouth is fully open
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("MouthFullyOpen"))
                {
                    // Only damage when the mouth is fully open (attack state)
                    PlayerStats playerStats = player.GetComponent<PlayerStats>(); // Get the PlayerStats component
                    playerStats.TakeDamage(damage); // Call TakeDamage from PlayerStats
                }
            }
        }
        else
        {
            // When the player is too far, return to normal behavior
            isInAttackRange = false;
            isAttacking = false;
            anim.SetBool("isAttacking", false); // Stop mouth opening
            anim.SetBool("isMouthFullyOpen", false); // Stop fully open animation
            anim.SetBool("isFacingPlayer", false); // Stop facing player
        }

        // If not attacking, keep moving normally
        if (!isAttacking)
        {
            anim.SetBool("isMoving", true); // Enemy is moving normally
        }

    }

    void FixedUpdate()
    {
       if (movingRight)
{
    temp_position.x += HorizontalSpeed * Time.deltaTime;
}
else
{
    temp_position.x -= HorizontalSpeed * Time.deltaTime;
}

        // Apply vertical wobble (using a sine wave for smooth up/down movement)
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
        isFacingPlayer = false; // Reset facing player status when flipping
        isFacingRight = !isFacingRight;  // Toggle facing direction
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Flip horizontally
    }

    // OnTriggerEnter2D: Trigger for damaging the player when in contact
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isInAttackRange && anim.GetCurrentAnimatorStateInfo(0).IsName("MouthFullyOpen"))
        {
            // Only deal damage if within attack range and mouth is fully open
            PlayerStats playerStats = other.GetComponent<PlayerStats>(); // Get PlayerStats component
            playerStats.TakeDamage(damage); // Call TakeDamage to reduce the player's health
        }
    }
    
}