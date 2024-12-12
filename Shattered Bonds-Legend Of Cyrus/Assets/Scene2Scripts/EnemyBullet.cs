using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed=3;
    private float lifeTime=4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed,GetComponent<Rigidbody2D>().velocity.y);
        lifeTime-=Time.deltaTime;
        if(lifeTime<=0){
            Destroy(this.gameObject);
        }
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
            other.GetComponent<PlayerStats>().TakeDamage(2);
            Destroy(this.gameObject);
        }
    }
}
