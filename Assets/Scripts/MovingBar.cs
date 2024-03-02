using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBar : MonoBehaviour
{
    public static bool enter = false;
    public GameObject cat;
    public GameObject mouse;
    public void Update()
    {
        Hit();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
       // Debug.Log("stay");
      
        enter = true;
        
       // Debug.Log(enter);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        enter = false;
        //Debug.Log("exit");
        //Debug.Log(enter);
    }

    public void Hit()
    {
        //Vector3 mousePosition = GameObject.Find("mouse").transform.position;
        
        if (enter && Input.GetButtonDown("Jump")){
           Debug.Log("HIT");

        }else if(!enter && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Missed");
            //reset game
        }
    }
}
