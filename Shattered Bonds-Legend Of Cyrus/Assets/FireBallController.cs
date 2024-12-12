
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    public float speed;
    public float timeremaining;
    private EzraController enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<EzraController>();
        if (enemy.transform.localScale.x < 0){
            speed = -speed;
            transform.localScale =new Vector3(-(transform.localScale.x), 
            transform.localScale.y, transform.localScale.z);
        }
          Debug.Log("bullet speed: hduee  " + speed);

        
    }

    // Update is called once per frame
    void Update()
    {
        
        GetComponent<Rigidbody2D>().velocity=new Vector2(speed,GetComponent<Rigidbody2D>().velocity.y);
        Debug.Log("eeubdwhgcewsi");
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

