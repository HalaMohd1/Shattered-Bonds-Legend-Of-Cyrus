using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerStats : MonoBehaviour
{
    public int health=6;
    public int lives=3; 
    public TextMeshProUGUI livesUI;
    private float flickerTime=0f;
    public float flickerDuration=0.1f;
    private SpriteRenderer spriteRenderer;
    public bool isImmune=false;
    private float immunityTime=0f;
    public float immunityDuration=1.5f;
    public int GemsCollected=0;
    public AudioClip GameOverSound;
    public TextMeshProUGUI ScoreUI;
    public Image healthBar;
    public Animator anim;
    public bool hasGem=false;
    bool IsRespawning=false;
    public AudioClip damaged;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        spriteRenderer=this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isImmune==true){
            Spriteflicker();
            immunityTime=immunityTime+Time.deltaTime;
            if(immunityTime>=immunityDuration){
                this.isImmune=false;
                this.spriteRenderer.enabled=true;
                
            }
        }
        if(this.transform.position.y<-10){
            TakeDamage(6);
        }
    }
    void Spriteflicker(){
        if(this.flickerTime<this.flickerDuration){
            this.flickerTime=this.flickerTime+Time.deltaTime;
        }
        else if(this.flickerTime>=this.flickerDuration){
            spriteRenderer.enabled=!(spriteRenderer.enabled);
            this.flickerTime=0;
        }
    }
    public void TakeDamage(int damage){
        if(this.isImmune == false){
            this.health=health-damage;
            healthBar.fillAmount=this.health/6f;
            if(health<0)
            {      
                health = 0;
            }
            if(this.lives>0 && this.health==0&&IsRespawning==false){
                IsRespawning=true;
                anim.SetBool("IsDead",true);
                this.lives--;
            }
            
            else if(this.lives ==0 && this.health ==0){
                anim.SetBool("IsDead",true);
                (new NavigationController()).GoToGameOverScene();
                FindObjectOfType<AudioManager>().PlaySingle(GameOverSound);
                Debug.Log("Gameover");
            }
            PlayHitReaction();
        Debug.Log("Player Health: "+ this.health.ToString());
        Debug.Log("Player Lives: "+ this.lives.ToString());
        }
         
    }
    void PlayHitReaction(){
        this.isImmune=true;
        this.immunityTime=0f;
        anim.SetBool("IsHurt",true);
        FindObjectOfType<AudioManager>().PlaySingle(damaged);
    }
    public void CollectGem(int GemValue){
        this.GemsCollected=this.GemsCollected+GemValue;
        ScoreUI.text=""+this.GemsCollected;
    }
    public void Respawn()
    {
        this.health=6;
        healthBar.fillAmount=this.health/6f;
        anim.SetBool("IsHurt",false);
        anim.SetBool("IsDead",false);
        FindObjectOfType<LevelManager>().RespawnPlayer();
        livesUI.text=""+this.lives+"x";
        IsRespawning=false;
    }
   public void StopHit()
   {
         anim.SetBool("IsHurt",false);
   }
}
