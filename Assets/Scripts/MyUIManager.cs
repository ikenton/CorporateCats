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
    public TextMeshProUGUI numOfMice;
    public RectTransform rt;
    public static float widthOfBar = 410f;
    public bool entered = false;
    public int levelNum = 1;
    public float speed = 200;
    public float greenWidth;
    public bool hit = false;
    public int miceCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        OffsetGreenArea();
        ChangeDifficulty();
        UpdateLevelText("Level ");
        UpdateMiceCountText("Mice Killed: ");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMiceCountText("Mice Killed: ");

        MoveBar();
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
    public void UpdateMiceCountText(string text)
    {
        numOfMice.text = "Mice killed: " + miceCount;
    }
    void MoveBar()
    {
        if (!hit)
        {
            slider.transform.position = new Vector3(Mathf.PingPong(Time.time * speed, widthOfBar) + 235f, 87f, transform.position.z);
            if (MovingBar.enter && Input.GetButtonDown("Jump"))
            {
                miceCount++;
                hit = true;
                
                CatPounce catPounce = FindObjectOfType<CatPounce>();
                if (catPounce != null)
                {
                    catPounce.MoveCatToMouse();
                }
            }
        }
        else
        {
            //reset game
        }
        
    }
}
