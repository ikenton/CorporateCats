using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBar : MonoBehaviour
{
    public Image greenArea;
    public Image slider;
    public Image qteBar;
    public static float widthOfBar = 300f;
    public float speed = 200;

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("stay");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("exit");
    }
}
