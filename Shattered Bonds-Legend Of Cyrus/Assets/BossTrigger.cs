using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject spawnPosition;
    public GameObject particleBossEntryEffect;
    public GameObject EzraBoss;
    GameObject particleEffectInst;



   
    // Start is called before the first frame update
    void Start()
    {
         
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player")
        {
            gameObject.SetActive(false); //remove trigger so it cannot be triggered again by player...basically triggering happens only once
            particleEffectInst = Instantiate(particleBossEntryEffect, spawnPosition.transform.position, Quaternion.identity);
            StartCoroutine(SpawningEzra());
           
        }
    }

    private IEnumerator SpawningEzra(){
        //GameObject particleEffect = Instantiate(particleBossEntryEffect, spawnPosition, Quaternion.identity); // Create an instance
            yield return new WaitForSeconds(2f); //make everything pause for a few secs
            Instantiate(EzraBoss,spawnPosition.transform.position,Quaternion.identity);//spawn ezra
            Destroy(particleEffectInst); //now destroy particle
           


    }
}
