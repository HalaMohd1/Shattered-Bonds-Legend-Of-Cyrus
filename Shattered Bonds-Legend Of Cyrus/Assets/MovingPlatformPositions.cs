using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPositions : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        transform.position=points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position,points[i].position)<0.02f){
            i++;
            if(i==points.Length){
                i=0;
            }
        }
        transform.position=Vector2.MoveTowards(transform.position,points[i].position,speed*Time.deltaTime);
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.collider.CompareTag("Player")){
            //if(transform.position.y<collision.transform.position.y-4.077599) 
                collision.transform.SetParent(transform);
                 collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, 0); //reset velocity to control slowmovement
    }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if (collision.collider.CompareTag("Player"))
        collision.transform.SetParent(null);
    }
}
