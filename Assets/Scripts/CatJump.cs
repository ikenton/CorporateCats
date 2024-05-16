using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatJump : MonoBehaviour
{
    public float travelVelocity;
    public float jumpVelocity;
    public Rigidbody2D cat;
    public ClimbUIController logic;

    public float jumpTimer = 0.0f;
    public float fastFallStartTime = 0.5f;
    
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
            Jump();
        }

        // Lets user drop cat midair 
        if (Input.GetKeyDown(KeyCode.DownArrow) && isJumping)
        {
               cat.velocity = Vector2.down * jumpVelocity;
        }
        if (isJumping)
        {
            if (jumpTimer > fastFallStartTime)
            {
                cat.velocity = Vector2.down * jumpVelocity;
            }
            else
            {
                jumpTimer += Time.deltaTime;
            }
        }
        else if (!isJumping)
        {
            jumpTimer = 0.0f;
        }
    }

    public void Jump()
    {
        cat.velocity = Vector2.up * jumpVelocity;
        isJumping = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isJumping)
            {
                Jump();

            }
            Debug.Log("Enemy entered");
        }
    }
}
