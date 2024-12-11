using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {

        if (this.isFacingRight == true)  // Check if the enemy is facing right
        {

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(maxSpeed, this.GetComponent<Rigidbody2D>().velocity.y);  // Set the enemy's horizontal velocity to the maximum speed

        }

        else
        {

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-maxSpeed, this.GetComponent<Rigidbody2D>().velocity.y); // Set the enemy's horizontal velocity to the negative maximum speed (Move left

        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Wall") // Check if the collision is with a wall
        {

            Flip();  // Flip the enemy's direction

        }

        else if (collider.tag == "Enemy") // Check if the collision is with a enemy
        {

            Flip();  // Flip the enemy's direction

        }

        if (collider.tag == "Player") // Check if the collision is with a player
        {

            FindObjectOfType<PlayerStats>().TakeDamage(damage);
            Destroy(this.gameObject);
            Flip();  // Flip the enemy's direction
        }

    }
}
