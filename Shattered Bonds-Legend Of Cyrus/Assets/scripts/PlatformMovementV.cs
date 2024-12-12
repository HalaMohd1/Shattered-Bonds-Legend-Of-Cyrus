
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementV : MonoBehaviour
{
    // Start is called before the first frame update
    public bool goingUp=true;
    public float speed=1f;
    public float startPosY;
    void Start()
    {
        startPosY=this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(goingUp==true){
                this.transform.position+=new Vector3(0,0.005f*speed,0);
                if(this.transform.position.y>=startPosY+2.5){
                    goingUp=false;
                }
            }
            if(goingUp==false){
                this.transform.position+=new Vector3(0,-0.005f*speed,0);
                if(this.transform.position.y<=startPosY-2.5){
                    goingUp=true;
                }
            }
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(this.transform);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
