using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMovePlatform : MonoBehaviour
{
    public float speed = 3f;
    public Transform startPoint; 
    public Transform endPoint; 
    private bool movingToEnd = true; 
    private bool isPlayerOnPlatform = false; 
    private Vector3 targetPosition; 

    void Start()
    {
          GetComponent<SpriteRenderer>().enabled = true; //not imp
        //setitng platform's initial position
        transform.position = startPoint.position;
        targetPosition = endPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isPlayerOnPlatform)
        {
            MovePlatform();
        }
    }


    void MovePlatform()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
           
            if (movingToEnd)
            {
                targetPosition = startPoint.position; 
            }
            else
            {
                targetPosition = endPoint.position;
            }
            movingToEnd = !movingToEnd; 
        }


        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = true; 
            collision.transform.SetParent(transform); 
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = false; 
            collision.transform.SetParent(null);
        }
    }
}
