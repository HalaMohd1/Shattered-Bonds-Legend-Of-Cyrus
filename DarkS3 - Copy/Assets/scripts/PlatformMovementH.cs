using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovementH : MonoBehaviour
{
    // Start is called before the first frame update
    public bool goingRight = true;
    float startPosX;
    public float speed = 1f;
    void Start()
    {
        startPosX = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (goingRight == true)
        {
            this.transform.position += new Vector3(0.001f*speed, 0, 0);
            if (this.transform.position.x >= (startPosX+3))
            {
                goingRight = false;
            }
        }
        if (goingRight == false)
        {
            this.transform.position += new Vector3(-0.001f*speed, 0, 0);
            if (this.transform.position.x <= startPosX)
            {
                goingRight = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(this.transform);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}