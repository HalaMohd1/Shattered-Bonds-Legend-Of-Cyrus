using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone1 : MonoBehaviour
{
    // Start is called before the first frame update
 private PlayerStatsHala playerStats; 

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStatsHala>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerStats.TakeDamage(playerStats.health); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
