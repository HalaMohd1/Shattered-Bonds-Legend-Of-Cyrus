//player controller
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{public float moveSpeed;
        public float jumpheight;
        public KeyCode Spacebar;
        public KeyCode L;
        public KeyCode R;
        public Transform groundCheck;
        public float groundCheckRadius;
        public LayerMask whatIsGround;
        private bool grounded;
        private Animator anim;

        /* public KeyCode Return;
        public Transform firepoint;
        public GameObject bullet;

        public AudioClip jump1;
         public AudioClip jump2;
         public AudioClip lasersound; */
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
       
      if(Input.GetKeyDown(Spacebar) && grounded)
      {
        Jump();
         anim.SetTrigger("JumpTrigger"); //the player jump animation occurs only once when jumping to avoid looping
        //AudioManager.instance.PlaySingle(jump1); //play one jump
        //AudioManager.instance.RandomizeSfx(jump1, jump2); //choose between teo jump sounds, randoms

      }
       if(Input.GetKey(L))
       {
           GetComponent<Rigidbody2D>().velocity=new Vector2(-moveSpeed,GetComponent<Rigidbody2D>().velocity.y);

           if(GetComponent<SpriteRenderer>()!=null)
            {
                GetComponent<SpriteRenderer>().flipX=true;

            }
        }

       else if(Input.GetKey(R))
         {
          GetComponent<Rigidbody2D>().velocity=new Vector2(moveSpeed,GetComponent<Rigidbody2D>().velocity.y);
        
         if(GetComponent<SpriteRenderer>()!=null)
         {
            GetComponent<SpriteRenderer>().flipX=false;
         }
         }
         else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
         }

         anim.SetFloat("Speed",Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
         anim.SetBool("Grounded", grounded);
         


         /* if(Input.GetKeyDown(Return)){
            Shoot();
         } */

    }

    /* public void Shoot(){
        Instantiate(bullet, firepoint.position, firepoint.rotation);
        AudioManager.instance.PlaySingle(lasersound);
    } */

     void FixedUpdate(){

      grounded=Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,whatIsGround);
     }

    void Jump()
    {
    GetComponent<Rigidbody2D>().velocity=new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpheight);
    }

}