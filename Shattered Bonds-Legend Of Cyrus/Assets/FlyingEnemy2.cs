using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy2 : EnemyController
{
    public float HorizontalSpeed;  
    public float VerticalSpeed;    //speed for the wobbling effect
    public float amplitude;        //the amplitude or height at which it moves between..
    private Vector3 temp_position; 
    public float attackRange = 2f; //range for detecting player
    private bool isAttacking = false; 

    private PlayerController player; 
    private Animator anim;           

    public Transform pointA;         //start point
    public Transform pointB;         //end point
    private bool movingRight = true; //flipp

    // Start is called before the first frame update
    void Start()
    {
        temp_position = transform.position;  
        anim = GetComponent<Animator>();     
        player = FindObjectOfType<PlayerController>(); 
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

        //for the wobble effect
        float verticalWobble = Mathf.Sin(Time.realtimeSinceStartup * VerticalSpeed) * amplitude;
        temp_position.y = pointA.position.y + verticalWobble;

        //updating its position(enemy)
        transform.position = temp_position;

        
        if (movingRight && temp_position.x >= pointB.position.x) //needed to flip the enemy once he reaches the end point
        {
            movingRight = false; //move it to the left
            Flip(); //call function for flipping
        }
        else if (!movingRight && temp_position.x <= pointA.position.x)
        {
            movingRight = true;  
            Flip(); //again call function for flipping
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
                anim.SetBool("isAttacking", true); //trigger or set the attack animation state...(transition) to attacking state
            }
        }
        else
        {
            if (isAttacking)
            {
                isAttacking = false;
                anim.SetBool("isAttacking", false); //set it back to normal..not the its attack state 
            }
        }
    }

    public new void Flip()
    {
    
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          
            PlayerStatsHala playerStats = other.GetComponent<PlayerStatsHala>();
                playerStats.TakeDamage(damage);
            
          
            Flip();
            movingRight = !movingRight; 
        }
    }
}
