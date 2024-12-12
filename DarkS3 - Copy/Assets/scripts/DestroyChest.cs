using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject forestGem;
    public void DestroyChests(){
        forestGem.SetActive(true);
        this.GetComponent<Animator>().enabled=false;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
