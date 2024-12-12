using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LadderBehav : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<Controller>().IsClimbing=true;
            other.gameObject.GetComponent<Controller>().grounded=true;
        }

    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<Controller>().IsClimbing=false;
        }
    }
}
