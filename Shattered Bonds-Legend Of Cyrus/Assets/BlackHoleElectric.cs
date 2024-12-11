using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleElectric : MonoBehaviour
{
    private PlayerStats playerstats;
    public int damage=4; //this way it should respawn player to checkpoint only from 1 collision
    // Start is called before the first frame update
    void Start()
    {
    
        playerstats=FindObjectOfType<PlayerStats>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerstats.TakeDamage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
