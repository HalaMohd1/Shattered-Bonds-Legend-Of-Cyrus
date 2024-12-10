
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickup : MonoBehaviour
{
    public int gemValue;
    public AudioClip coinSound;
    public AudioClip coinSound2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
            FindObjectOfType<PlayerStats>().CollectGem(gemValue);
           
            //AudioManager.instance.PlaySingle(coinSound);
            //AudioManager.instance.RandomizeSfx(coinSound,coinSound2);
            Destroy(this.gameObject);
            Debug.Log("Coin Collected!");
            Debug.Log("Coin Value: " + gemValue);

        }
    }
}
