using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrifiedPlatforms : MonoBehaviour
{
    public float speed;                  // Speed of the platform
    public Transform[] points;           // Points the platform will move between
    private int i = 0;                   // To track the current point

    public GameObject lightningPrefab;   // Reference to the lightning prefab
    public float electrifiedDuration = 5f; // Duration before electrification happens again
    private bool isElectrified = false;  // Is the platform electrified?
   public int damage;
    private GameObject lightningEffect;  // Reference to the instantiated lightning effect

    // Start is called before the first frame update
    void Start()
    {
        // Start the electrification timer
        InvokeRepeating("ToggleElectrification", 0f, electrifiedDuration);
        
        // Start the platform at the first point
        transform.position = points[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform between points
        MovePlatform();
    }

    // Handle platform movement between points
    void MovePlatform()
    {
        // Check if the platform reached the current point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length) i = 0;  // Loop back to the first point if reached the last point
        }

        // Move the platform towards the next point
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

        // If the platform is electrified, ensure the lightning follows it
        if (lightningEffect != null)
        {
            lightningEffect.transform.position = transform.position; // Make sure lightning effect follows the platform
        }
    }

    // Toggle the electrified state
    void ToggleElectrification()
    {
        isElectrified = !isElectrified;

        // If the platform is electrified, instantiate the lightning effect
        if (isElectrified)
        {
            // Instantiate the lightning effect at the platform's position
            lightningEffect = Instantiate(lightningPrefab, transform.position, Quaternion.identity);
            lightningEffect.transform.SetParent(transform); // Ensure the lightning effect moves with the platform

            // Destroy the lightning effect after a short time (e.g., 1 second)
            Destroy(lightningEffect, 3f);  // You can change 1f to the duration you want the lightning to stay
        }
    }

       private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Attach the player to the platform

            if (isElectrified) // Check if the platform is electrified
            {
                // Player takes damage if the platform is electrified
                PlayerStatsHala playerStats = collision.collider.GetComponent<PlayerStatsHala>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(damage);  // Deal damage to the player
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && isElectrified)
        {
            // Player continuously takes damage if the platform stays electrified
            PlayerStatsHala playerStats = collision.collider.GetComponent<PlayerStatsHala>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);  // Damage over time while staying on the platform
            }
        }
    }

   
     void OnCollisionExit2D(Collision2D collision){
        if (collision.collider.CompareTag("Player"))
        collision.transform.SetParent(null);
    }
}


