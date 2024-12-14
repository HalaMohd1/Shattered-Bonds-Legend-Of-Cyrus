using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float shootingTimer=0;
    public float moveSpeed;
    public float jumpHeight;
    public KeyCode Spacebar;
    public KeyCode L;
    public KeyCode R;
    public Transform grounCheck;
    public float grounCheckRadius;
    public LayerMask whatIsGround;
    public bool IsClimbing;
    public bool grounded;
    private Animator anim;
    public KeyCode Return;
    public AudioClip ArrowShootingSound;
    public AudioClip JumpSound;
    public Transform firepoint;
    public GameObject bullet;
    public BoxCollider2D boxCollider;
    public Object SwordAttackObj;
    private float speed;
    private int arrowCount=0;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        boxCollider=GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
       speed=moveSpeed*anim.GetFloat("Speed");
       if(Input.GetKeyDown(Spacebar) && grounded){
            Jump(); 
            FindObjectOfType<AudioManager>().PlaySingle(JumpSound);
        }
        else if(Input.GetKey(KeyCode.C) && grounded){

            anim.SetBool("IsCrouching", true);
            moveSpeed=0;
            Vector2 newSize = boxCollider.size; 
            newSize.y = 0.49f;                        
            boxCollider.size = newSize; 
            boxCollider.offset=new Vector2(-0.03f,0.3f);
            Debug.Log(boxCollider.size);
        }
        else{
            Debug.Log("not crouching");
            anim.SetBool("IsCrouching", false);
            moveSpeed=5;
            Vector2 newSize = boxCollider.size; 
            newSize.y = 0.9712492f;                        
            boxCollider.size = newSize;
            boxCollider.offset=new Vector2(-0.03f,0.54f);         
        }
        if(IsClimbing){ 
            anim.SetBool("IsClimbing", true);          
            GetComponent<Rigidbody2D>().velocity=new Vector2(0,Input.GetAxis("Vertical")*moveSpeed);
        }
        else {
            anim.SetBool("IsClimbing", false);
        }
        if(Input.GetKey(Return) && arrowCount>0){
                anim.SetBool("IsShooting", true);
                 FindObjectOfType<AudioManager>().PlaySingle(ArrowShootingSound);
         }
         else if(Input.GetKeyDown(KeyCode.F)){
            anim.SetBool("IsAttacking", true);
         } 
      
        if(Input.GetKey(L)){

            anim.SetFloat("Speed",Mathf.Clamp(anim.GetFloat("Speed") + Time.deltaTime*2, 0, 1) ); 
            GetComponent<Rigidbody2D>().velocity=new Vector2(-speed,GetComponent<Rigidbody2D>().velocity.y);
                if( GetComponent<SpriteRenderer>()!=null){
                    GetComponent<SpriteRenderer>().flipX=true;
                    SwordAttackObj.GetComponent<Transform>().localScale = new Vector3(-1, 1, 1);
                }
        }
        else if(Input.GetKey(R)){
                anim.SetFloat("Speed",Mathf.Clamp(anim.GetFloat("Speed") + Time.deltaTime, 0, 1) );
                 GetComponent<Rigidbody2D>().velocity=new Vector2(speed,GetComponent<Rigidbody2D>().velocity.y);
                          if( GetComponent<SpriteRenderer>()!=null){
                    GetComponent<SpriteRenderer>().flipX=false;
                    SwordAttackObj.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                }       
         }
        else{
             GetComponent<Rigidbody2D>().velocity=new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
            anim.SetFloat("Speed",Mathf.Clamp(anim.GetFloat("Speed") - Time.deltaTime*2, 0, 1) );
         }
       
          anim.SetBool("IsGrounded", grounded);
    }
    void Jump(){
        GetComponent<Rigidbody2D>().velocity= new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight);

    }
    public void Shoot(){
            Instantiate(bullet, firepoint.position,firepoint.rotation);
            arrowCount--;
            anim.SetBool("IsShooting", false);
         }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag=="Arrow"){
            arrowCount+=5;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag=="Health"){
            Destroy(collision.gameObject);
            if(this.GetComponent<PlayerStats>().health<6)
            {
                this.GetComponent<PlayerStats>().health+=2;
                this.GetComponent<PlayerStats>().healthBar.fillAmount=this.GetComponent<PlayerStats>().health/6f;
            }
        }
        else if(collision.gameObject.tag=="ExtraLife"){
            Destroy(collision.gameObject);
            this.GetComponent<PlayerStats>().lives++;
            this.GetComponent<PlayerStats>().livesUI.text=this.GetComponent<PlayerStats>().lives.ToString()+"x";
            
        }
    }
    void FixedUpdate(){
        grounded=Physics2D.OverlapCircle(grounCheck.position,grounCheckRadius,whatIsGround);

   }
    public void StopAttacking(){
        anim.SetBool("IsAttacking", false);
    }
}
    

    
