using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleTeleport : MonoBehaviour
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
        if(other.tag=="Player"){
            Debug.Log("Teleport");
            if(other.GetComponent<PlayerStats>().hasGem==true) Application.LoadLevel(1);
        }
    }
}
