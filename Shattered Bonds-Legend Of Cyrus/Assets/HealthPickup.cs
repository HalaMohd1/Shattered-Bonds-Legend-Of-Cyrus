using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

     public int healAmount = 2; //health amount

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
       
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            playerStats.HealthIncrease(healAmount); 
            
            Destroy(gameObject); //destroy after pickup
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
