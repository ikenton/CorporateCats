using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            desiredx = -4f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            desiredx = 4f;
        }
        else
        {
            desiredx = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Collided with NPC");
        }
    }
}
