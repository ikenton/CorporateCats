using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMouse : MonoBehaviour
{
    public Rigidbody2D mouse;
    public float deadZone = -12;
    public float travelVelocity = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouse.velocity += Vector2.left * travelVelocity * Time.fixedDeltaTime;
        if (transform.position.x < deadZone){
            Destroy(gameObject);
        }
    }
}
