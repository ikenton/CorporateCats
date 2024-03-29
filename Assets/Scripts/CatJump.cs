using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatJump : MonoBehaviour
{
    public float travelVelocity;
    public float jumpVelocity;
    public Rigidbody2D cat;
    public ClimbUIController logic;
    
    public bool isJumping = false;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<ClimbUIController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // cat.velocity = Vector2.right * travelVelocity;  
        if ((Input.GetKeyDown(KeyCode.Space) == true || Input.GetKeyDown(KeyCode.UpArrow)) && isJumping == false){   
            cat.velocity = Vector2.up * jumpVelocity;
            isJumping = true;
        }

        // Lets user drop cat midair 
        if (Input.GetKeyDown(KeyCode.DownArrow) && isJumping)
        {
               cat.velocity = Vector2.down * jumpVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;

        if (collision.gameObject.name == "Mouse(Clone)")
        {
            logic.CompletedClimb();
            Debug.Log("DIE");
        }
    }
}
