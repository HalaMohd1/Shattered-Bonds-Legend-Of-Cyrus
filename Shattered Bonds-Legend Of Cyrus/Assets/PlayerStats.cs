
//player stats
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 4;
    public int lives = 8;

    private float flickerTime = 0f;//timer player remains visible/inv.
    public float flickerDuration = 0.1f; //flicker speed

    private SpriteRenderer spriteRenderer;

    public bool isImmune = false;
    private float immunityTime = 0f;//timer player is immune
    public float immunityDuration = 1.5f; //time player remains immuneafterdamage

    public int gemsCollected = 0;
    public AudioClip gameoversound;
    public AudioClip gamevictorysound;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        
    }
    public void CollectGem(int gemValue){
        this.gemsCollected=this.gemsCollected + gemValue;
    }


    void SpriteFlicker()
    {

        if (this.flickerTime < this.flickerDuration)
        {
            this.flickerTime = this.flickerTime + Time.deltaTime;
        }
        else if(this.flickerTime >= this.flickerDuration)
        {
            spriteRenderer.enabled = !(spriteRenderer.enabled);
            this.flickerTime = 0;
        }

    }



    public void TakeDamage(int damage) { 
    
    if(this.isImmune == false)
        {
            this.health = this.health - damage;
            if(health < 0)
                this.health = 0;
            if(this.lives > 0 && this.health == 0)
            {
                FindObjectOfType<LevelManager>().RespawnPlayer();
                this.health = 4;
                this.lives--;
            }
            else if(this.lives == 0 && this.health == 0)
            {
                Debug.Log("Gameover"); //print gameover on console
                Destroy(this.gameObject); //remove player from game
             //   AudioManager.instance.PlaySingle(gameoversound);
            }


            Debug.Log("Player Health:" + this.health.ToString());
            Debug.Log("Player lives:" + this.lives.ToString());
        }

        PlayHitReaction();
    
    }


    void PlayHitReaction()
    {
        this.isImmune = true;
        this.immunityTime = 0f;
    }



    // Update is called once per frame
    void Update()
    {
     if(this.isImmune == true)
        {
            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;
            if(immunityTime >= immunityDuration)
            {
                this.isImmune = false;
                this.spriteRenderer.enabled = true;
                Debug.Log("immunity has ended");
            }
        }   
    }

/*  public void GameWin()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
                    //count enemies defeated
        if (enemies.Length == 0) 
        {
            Debug.Log("Game Victory! You Win!");
// AudioManager.instance.PlaySingle(gamevictorysound);
        }
    } */

}
