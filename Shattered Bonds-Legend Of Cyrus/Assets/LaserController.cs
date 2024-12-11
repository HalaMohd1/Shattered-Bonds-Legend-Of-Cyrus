
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float speed;
    public float timeremaining;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (player.transform.localScale.x < 0){
            speed = -speed;
            transform.localScale =new Vector3(-(transform.localScale.x), 
            transform.localScale.y, transform.localScale.z);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
        GetComponent<Rigidbody2D>().velocity=new Vector2(speed,GetComponent<Rigidbody2D>().velocity.y);
        
        if (timeremaining > 0){
            timeremaining=timeremaining - Time.deltaTime;
        }
        else if(timeremaining <= 0){
            Destroy(this.gameObject);
        }
        
    }


    void OnTriggerEnter2D(Collider2D other){
        if (other.tag=="Enemy"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}

