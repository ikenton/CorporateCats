using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPounce : MonoBehaviour
{
    public Transform mouseTransform; 
    public float speed = 20f;
    
    public void MoveCatToMouse()
    {
        if (mouseTransform != null)
        {
            //have it slide into position
            transform.position = mouseTransform.position;
        }
        else
        {
            Debug.LogError("Mouse Transform reference is not set!");
        }
    }
}
