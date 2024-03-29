using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlBeingFilled : MonoBehaviour
{

    [SerializeField] private AudioClip dropClip, finish;
    public Sprite biscuit;
    public GameObject spoon;

    private bool allIngredientsAdded = false;
    public int x = 0;
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Touched.");
        if (col.gameObject.name == "flour" || col.gameObject.name == "milk" || col.gameObject.name == "egg")
        {
            x++;
            Debug.Log("Touched.");
            Destroy(col.gameObject);
            AudioSource.PlayClipAtPoint(dropClip, transform.position);

            if (x==3)
            {
                // Destroy(spoon.GameObject);
                Transform biscuitTransform = this.gameObject.GetComponent<Transform>();
                biscuitTransform.localScale = new Vector3(9f, 9f, 1f);
                this.gameObject.GetComponent<SpriteRenderer>().sprite = biscuit;
                AudioSource.PlayClipAtPoint(finish, transform.position);
            }
        }
    }
}
 