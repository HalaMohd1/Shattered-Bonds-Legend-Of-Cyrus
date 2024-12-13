using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float speed = 10f; // Adjustable in Inspector
    public float timeremaining = 3f; // Laser lifetime
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set the laser velocity based on speed
        //rb.velocity = new Vector2(speed, 0);

        Debug.Log("Laser Speed Set: " + speed);
    }

    void Update()
    {
        // Timer to destroy the laser
        timeremaining -= Time.deltaTime;
        if (timeremaining <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroy enemy
            Destroy(gameObject); // Destroy laser
        }
    }
}
