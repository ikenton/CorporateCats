using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MySlider : MonoBehaviour
{
    public Image greenArea;
    public Image slider;
    public Image qteBar;
    public TextMeshProUGUI level;
    public RectTransform rt;
    public static float widthOfBar = 410f;
    public float xPosition; /*this will be used when the length of 
                             * the qte bar is changed. It is also a
                             * temp val rn so the slider appropriately lines up*/
    public float yPosition;
    public bool entered = false;
    public int levelNum = 1;
    public float speed = 200;

    // Start is called before the first frame update
    void Start()
    {
        OffsetGreenArea();
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = new Vector3(Mathf.PingPong(Time.time * speed, widthOfBar) +235f, 87f, transform.position.z);

    }

    void OffsetGreenArea()
    {
        rt = greenArea.GetComponent<RectTransform>();
        float offsetx = 0f;
        offsetx = RandomNumberGenerator.GetInt32(-130,120);
        Vector2 currentPosition = rt.anchoredPosition;
        currentPosition.x += offsetx;
        rt.anchoredPosition = currentPosition;

    }
}
