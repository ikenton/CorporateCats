using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            desiredx = -3f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            desiredx = 3f;
        }
        else
        {
            desiredx = 0f;
        }
    }
}
