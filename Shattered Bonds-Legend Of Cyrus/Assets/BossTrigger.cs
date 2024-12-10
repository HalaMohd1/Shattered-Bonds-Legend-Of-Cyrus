using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject particleBossEntryEffect;
    public GameObject EzraBoss;
    PlayerController player;
    private Vector2 spawnPosition;


   
    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<PlayerController>();
         
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player")
        {
            Vector2 spawnOffset = new Vector2(2f, 0f);//calc position where boss&particle appears
            Vector2 spawnPosition = (Vector2)player.transform.position + spawnOffset;
             gameObject.SetActive(false); //remove trigger so it cannot be triggered again by player...basically triggering happens only once
            StartCoroutine(SpawningEzra());
           
        }
    }

    private IEnumerator SpawningEzra(){
        GameObject particleEffect = Instantiate(particleBossEntryEffect, spawnPosition, Quaternion.identity); // Create an instance
        yield return new WaitForSeconds(5f); // Wait for a few seconds
         Instantiate(particleBossEntryEffect,spawnPosition,Quaternion.identity); //instantiate particle effect
            yield return new WaitForSeconds(5f); //make everything pause for a few secs
            Instantiate(EzraBoss,spawnPosition,Quaternion.identity);//spawn ezra
            Destroy(particleEffect); //now destroy particle
           


    }
}
