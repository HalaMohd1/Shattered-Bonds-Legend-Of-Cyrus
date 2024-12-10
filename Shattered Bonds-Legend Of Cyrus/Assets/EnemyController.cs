
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public bool isFacingRight = false;
    public float maxSpeed = 3f;
    public int damage = 6;

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D other) { 
        /*when enemy cllide with player..the player receive 
        damage .reduce health..from playerstats*/
        if(other.tag == "Player")
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
        }
    
    
    }

/* private void OnDestroy()
{
    EnemyDestroyed(); //call enemyDestroyed func when the enemy is destroyed
} */

 /* private void EnemyDestroyed() 
    {////call when enemy destroyed
        PlayerStats playerstatsobj= FindObjectOfType<PlayerStats>();
        playerstatsobj.GameWin();
    } */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}