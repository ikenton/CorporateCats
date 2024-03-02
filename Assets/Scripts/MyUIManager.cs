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
    public bool entered = false;
    public int levelNum = 1;
    public float speed = 200;
    public float greenWidth;

    // Start is called before the first frame update
    void Start()
    {
        OffsetGreenArea();
        ChangeDifficulty();
        UpdateLevelText("Level ");
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

    public void ChangeDifficulty()
    {
        rt = greenArea.GetComponent<RectTransform>();
        greenWidth = rt.rect.size.x;

        if(levelNum > 1)
        {
            speed = levelNum*100 +50;
            Vector2 sizeDelta = rt.sizeDelta;
            float newWidth = greenWidth /levelNum ;
            sizeDelta.x = newWidth;
            rt.sizeDelta = sizeDelta;
        }
    }

    public void UpdateLevelText(string text)
    {
        level.text = "Level " + levelNum;
    }
}
