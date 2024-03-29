using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragged : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pickUpCLip, dropClip;
    private Color mouseOverColor = Color.red;
    private bool dragging;
    private Vector2 offset;
    private Vector2 originalPosition;
    void Awake() 
    {
        originalPosition = transform.position;
    }
    void Update()
    {
        if(!dragging) return;

        var mousePosition = GetMousePos();
        transform.position = mousePosition - offset;
    }
    void OnMouseEnter()
    {
        // GetComponent<Renderer>().material.color = mouseOverColor;
        Debug.Log("Mouse entered.");
    }

    void OnMouseDown()
    {
        dragging = true;
        // source.PlayOneShot(pickUpClip);
        offset = GetMousePos() - (Vector2)transform.position;
        Debug.Log("Mouse down.");
    }

    void OnMouseUp()
    {
        // transform.position = originalPosition;
        dragging = false;
        // source.PlayOneShot(dropClip);
        Debug.Log("Mouse up.");
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
