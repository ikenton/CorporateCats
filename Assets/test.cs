using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
   public float speed = 1.0f;

  
    void Update()
    {
        // Move the GameObject to the right
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Check if the GameObject is now off the screen
        if (transform.position.x > Screen.width)
        {
            // Reset its position to the left
            transform.position = new Vector3(-Screen.width, transform.position.y, transform.position.z);
        }
    }
}
   