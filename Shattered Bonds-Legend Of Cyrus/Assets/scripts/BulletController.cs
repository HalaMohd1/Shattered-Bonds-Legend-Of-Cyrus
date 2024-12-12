using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float lifeTime=3;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(player==null){
            player=GameObject.Find("Player");
        }
        if(player.GetComponent<SpriteRenderer>().flipX==true){
            speed=-speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity=new Vector2(speed,GetComponent<Rigidbody2D>().velocity.y);
        lifeTime-=Time.deltaTime;
        if(lifeTime<=0){
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="EnemyWater"){
            Destroy(other.gameObject);
                Destroy(this.gameObject);
      }
        else if(other.tag=="Enemy"){
            other.GetComponent<WalkingEnemy>().TakeDamage(6);
            Destroy(this.gameObject);
        }
      else if(other.tag=="Boss"){
          other.GetComponent<Boss_Controller>().health-=5;
          Destroy(this.gameObject);
      }
    }
}