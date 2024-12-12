using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public int health=30;
    public float maxY;
    public float minY;
    public GameObject bullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    private float direction = 1.0f;
    public float speed = 2.0f;
    public float shootingTimer=2f;


    void Update()
    {
        if (health <= 0)
        {
            FindObjectOfType<NavigationController>().GoToVictoryScene();
            Destroy(this.gameObject);
        }

        shootingTimer-=Time.deltaTime;
        transform.Translate(Vector2.up * speed * direction * Time.deltaTime);

        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
            direction = -1.0f; 
        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, minY);
            direction = 1.0f; 
        }
        if(shootingTimer<=0){
            Instantiate(bullet,transform.position,Quaternion.identity);
            shootingTimer=2f;
        }
    }
}
