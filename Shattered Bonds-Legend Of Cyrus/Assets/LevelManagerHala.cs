
//level manager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerHala : MonoBehaviour
{
public Transform enemy;
public GameObject CurrentCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer(){
        FindObjectOfType<PlayerController>().transform.position=CurrentCheckpoint.transform.position;
        Debug.Log("respawned");
    }

    public void RespawnEnemy(){
        //enemy will be respawned at level manager gameobject position
        //Instantiate(enemy, transform.position, transform.rotation);
         GameObject spawnedEnemy = Instantiate(enemy.gameObject, transform.position, transform.rotation);

        // Ensure the spawned enemy faces the correct direction (facing right)
        // Set localScale to positive to face right (or flip it based on your needs)
        spawnedEnemy.transform.localScale = new Vector3(Mathf.Abs(spawnedEnemy.transform.localScale.x), 
                                                         spawnedEnemy.transform.localScale.y, 
                                                         spawnedEnemy.transform.localScale.z);
    }
}