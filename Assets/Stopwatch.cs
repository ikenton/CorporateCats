using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private bool isRunning = false;
    private float timeElapsed = 0;
    [SerializeField] private TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        StartStopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            timeText.text = "Score: " + timeElapsed.ToString("F2");
        }
    }
    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }
}
