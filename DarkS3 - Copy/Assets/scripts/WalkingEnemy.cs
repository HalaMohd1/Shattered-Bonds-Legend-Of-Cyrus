using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WalkingEnemy : EnemyController
{
    public float maxX;
    public float minX;
    Animator anim;
    bool isSlashing=false;
    public int health=6; 
    private bool isImmune=false;
     public AudioClip enemyHit;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        if(this.isFacingRight==true && isSlashing==false && isImmune==false){
            this.GetComponent<Rigidbody2D>().velocity=new Vector2(maxSpeed,this.GetComponent<Rigidbody2D>().velocity.y);
            if(this.transform.position.x>=maxX){
                Flip();
            }
        }
        else if(isSlashing==false && isImmune==false){ {
            this.GetComponent<Rigidbody2D>().velocity=new Vector2(-maxSpeed,this.GetComponent<Rigidbody2D>().velocity.y);
            if(this.transform.position.x<=minX){
                Flip();
            }
        }
        }
    }
    
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag=="Wall"){
            Flip();
        }
        else if(collider.tag=="Player"&& collider.GetComponent<PlayerStats>().isImmune==false){
            this.GetComponent<Rigidbody2D>().velocity=new Vector2(0,this.GetComponent<Rigidbody2D>().velocity.y);
            Debug.Log("Player Detected");
            anim.SetBool("IsSlashing",true);
            isSlashing=true;
            FindObjectOfType<AudioManager>().PlaySingle(enemyHit);
        }
    }
    public void StopSlashing(){
        anim.SetBool("IsSlashing",false);
        isSlashing=false;
    }
    public void TakeDamage(int damage){
        health-=damage;
        if(!isImmune){
            isImmune=true;   
            this.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        if(health<=0){
            anim.SetBool("IsDead",true);
            
        }
        else{

            anim.SetBool("IsHurt",true);
        }
        }
    }
    public void StopHurting(){
        anim.SetBool("IsHurt",false);
        isImmune=false;
    }
    public void DestroyEnemy(){
        Destroy(this.gameObject);
    }

}
