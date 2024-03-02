using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBar : MonoBehaviour
{
    public bool enter = false;

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
        if (enter && Input.GetButtonDown("Jump")){
            Debug.Log("HIT");
        }
    }
}
