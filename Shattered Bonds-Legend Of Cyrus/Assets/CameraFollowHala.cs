//Camera Follow
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHala : MonoBehaviour
{
    public Transform Target;
    public float Cameraspeed;
    public float minX ,maxX , minY , maxY ;
    public float offset=0;
    // Start is called before the first frame update
    void Start()
    {        
        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        if (Target != null)
        {
            Vector2 newCamPosition = Vector2.Lerp(transform.position, Target.position, Time.deltaTime * Cameraspeed);
            float ClampX = Mathf.Clamp(newCamPosition.x, minX, maxX);
            float ClampY = Mathf.Clamp(newCamPosition.y, minY, maxY);
            transform.position = new Vector3(ClampX, ClampY+offset, -10f);
        }

        

    }
}
