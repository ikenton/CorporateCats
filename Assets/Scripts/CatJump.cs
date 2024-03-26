using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatJump : MonoBehaviour
{
    public float travelVelocity;
    public float jumpVelocity;
    public Rigidbody2D cat;

    public bool isJumping = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // cat.velocity = Vector2.right * travelVelocity;  
        if (Input.GetKeyDown(KeyCode.Space) == true && isJumping == false){   
            cat.velocity = Vector2.up * jumpVelocity;
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }
}
