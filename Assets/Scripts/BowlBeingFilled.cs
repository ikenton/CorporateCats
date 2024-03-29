using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlBeingFilled : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Touched.");
        if (col.gameObject.name == "flour" || col.gameObject.name == "milk" || col.gameObject.name == "egg")
        {
            Debug.Log("Touched.");
            Destroy(col.gameObject);
        }
    }
}
