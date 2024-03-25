using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPounce : MonoBehaviour
{
    public Transform mouseTransform;
    public float speed = 100f;
    public static bool pounced = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        //pounced = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //pounced = false;
        //Debug.Log("exit");
        
    }
}
