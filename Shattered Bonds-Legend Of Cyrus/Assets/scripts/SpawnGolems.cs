using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnGolems : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Golem;
    public List<GameObject> Golems;
    public float spawnTime=10f;
    public int maxGolems=3;
    private float spawnTimer=0f;
    // Start is called before the first frame update
    void Start()
    {
        Golems = new List<GameObject>(maxGolems);
    }

    // Update is called once per frame
    void Update()
    {
        Golems.RemoveAll(golem => golem == null);


        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTime && Golems.Count < maxGolems)
        {

            GameObject newGolem = Instantiate(Golem, transform.position, transform.rotation);
            Golems.Add(newGolem);
            spawnTimer = 0f;
        }
    }
    
}
