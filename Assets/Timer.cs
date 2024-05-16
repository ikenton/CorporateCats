using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool isRunning = false;
    public float timeLeft = 60;
    public TMP_Text timeText;
    public MySlider uiManager;
    // Start is called before the first frame update
    void Start()
    {
        if (InterviewManager.Instance.isAutoplay)
        {
            timeLeft = 10;
            StartTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = "Time Left: " + timeLeft.ToString("F2");
            if (timeLeft <= 0)
            {
                isRunning = false;
                uiManager.GetComponent<MySlider>().SendMessage("CompletedPounce");
            }
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
