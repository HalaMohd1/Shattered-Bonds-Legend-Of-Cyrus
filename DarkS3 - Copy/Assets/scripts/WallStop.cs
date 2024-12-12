using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Boss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss.GetComponent<WalkingEnemy>().health<=0){
            Destroy(this.gameObject);
        }
    }
}
