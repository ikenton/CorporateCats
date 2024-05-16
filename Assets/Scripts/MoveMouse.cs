using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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
        float scoreDiff = Stopwatch.timeElapsed / 2;
        //print(scoreDiff);
        if (scoreDiff < 1){
            mouse.velocity += Vector2.left * travelVelocity * Time.fixedDeltaTime;
        }
        else{
            mouse.velocity += Vector2.left * travelVelocity * Time.fixedDeltaTime * scoreDiff;
        }
        if (transform.position.x < deadZone){
            Destroy(gameObject);
        }
    }
}
