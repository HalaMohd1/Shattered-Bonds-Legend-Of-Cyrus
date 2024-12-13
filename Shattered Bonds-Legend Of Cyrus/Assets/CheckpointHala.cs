
//checkpoint
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        /*When player enter checkpoint trigger... 
        it updates the LevelManager’s CurrentCheckpoint reference 
        to new/current checkpoint collided with*/
        if( other.tag=="Player"){
            FindObjectOfType<LevelManagerHala>().CurrentCheckpoint=this.gameObject;
        }
    }



}


