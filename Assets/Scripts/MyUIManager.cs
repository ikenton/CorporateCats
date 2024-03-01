using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MySlider : MonoBehaviour
{
    public Image greenArea;
    public Image slider;
    public Image qteBar;
    public static float widthOfBar = 300f;
    public float xPosition; /*this will be used when the length of 
                             * the qte bar is changed. It is also a
                             * temp val rn so the slider appropriately lines up*/
    public float yPosition;
    public bool entered = false;
    public float speed = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = new Vector3(Mathf.PingPong(Time.time * speed, widthOfBar) + 300f, transform.position.y - 36f, transform.position.z);

    }


}
