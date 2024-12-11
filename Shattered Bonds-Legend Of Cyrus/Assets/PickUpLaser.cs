using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLaser : MonoBehaviour
{
    PlayerController player; //reference to playerconttroller script
    bool LaserPickedUp =false;


    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<PlayerController>();
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag=="Player"){
            player.LaserPickedUp=true;
            Destroy(gameObject); //destroy laser to ensure laser picked it up
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
