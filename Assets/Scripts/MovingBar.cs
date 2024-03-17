using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBar : MonoBehaviour
{
    public static bool enter = false;
    public GameObject cat;
    public GameObject mouse;
    

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

   
}
