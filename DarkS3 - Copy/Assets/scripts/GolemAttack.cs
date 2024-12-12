using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    public int damage=2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider){
        Debug.Log("Golem Attack");
        if(collider.tag=="Player"){
            collider.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
