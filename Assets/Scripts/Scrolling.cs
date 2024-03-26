using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed;
    [SerializeField] private Renderer bgRenderer;
    void Update()
    {
        bgRenderer.material.mainTextureOffset+= new Vector2(speed * Time.deltaTime, 0);
    }
}
