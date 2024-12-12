using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public GameObject CurrentCheckpoint;
    public Transform enemy;
    public GameObject[] platforms;
    public bool ifHasPlatforms=true;

    Vector2[] initPos;
    // Start is called before the first frame update
    void Start()
    {
      initPos = new Vector2[platforms.Length];
      int i=0;
      foreach(GameObject platform in platforms){
        initPos[i] = platform.transform.position;
        i++;
      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RespawnPlayer(){
      FindObjectOfType<Controller>().transform.position=CurrentCheckpoint.transform.position;
      if(ifHasPlatforms){
      
      int i=0;
      foreach(GameObject platform in platforms){
        platform.transform.position = initPos[i];
        platform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        i++;
      }
      }
      
    }
     public void RespawnEnemy(){
        Instantiate(enemy,transform.position,transform.rotation);
     }
}