using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzraController : MonoBehaviour
{
    public float moveSpeed = 3f;           // Movement speed
    public Transform leftPoint, rightPoint; // Patrol points
    public GameObject fireball;   // The projectile to shoot
    public Transform firePoint;           // Where the projectile will spawn
    public float stopDuration = 2f;       // How long the enemy stops at each point
    public float shootingInterval = 1f;   // How often the enemy shoots while stopped

    private bool movingRight = true;      // Direction flag
    private bool isStopped = false;       // Tracks if the enemy is currently stopped
   // private SpriteRenderer sr; // For flipping the enemy sprite
    private Animator anim;                // Animator reference

    // Start is called before the first frame update
    void Start()
    {
       // sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transform.position = leftPoint.position; // Start at the left point
 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped)
        {
            MoveBetweenPoints();
        }
    }
     void MoveBetweenPoints()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            // Check if reached the right point
            if (Vector2.Distance(transform.position, rightPoint.position) < 0.1f)
            {
                StartCoroutine(StopAndShoot());
                movingRight = false; // Change direction
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            // Check if reached the left point
            if (Vector2.Distance(transform.position, leftPoint.position) < 0.1f)
            {
                StartCoroutine(StopAndShoot());
                movingRight = true; // Change direction
                Flip();
            }
        }

        anim.SetFloat("Speed",Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    IEnumerator StopAndShoot()
    {
        isStopped = true; // Stop movement
        anim.SetFloat("Speed",Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

        // Start shooting while stopped
        float timer = stopDuration;
        while (timer > 0)
        {
            Shoot();
            timer -= shootingInterval;
            yield return new WaitForSeconds(shootingInterval);
        }

        isStopped = false; // Resume movement
    }

    void Shoot()
    {
        // Instantiate the projectile at the fire point
        Instantiate(fireball, firePoint.position, firePoint.rotation);

        // Trigger shooting animation if needed
        if (anim != null)
        {
            anim.SetTrigger("Shoot");
        }
    }

    void Flip()
    {
        // Flip the sprite horizontally
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;

        // Adjust the firePoint position for the new direction
        Vector3 firePointScale = firePoint.localScale;
        firePointScale.x *= -1; // Flip firePoint horizontally
        firePoint.localScale = firePointScale;
    }








}
