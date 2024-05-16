using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClimbUIController : MonoBehaviour
{
    // public Stopwatch score;
    public TextMeshProUGUI highScore;
    public GameObject completedPopUp;
    public Button back;
    public Button next;
    public int goalTime;    // AHH HARDCODED
    // Start is called before the first frame update
    private void Start()
    {
        next.gameObject.SetActive(false);
        if (InterviewManager.Instance != null)
        {
            if (InterviewManager.Instance.isAutoplay)
            {
                next.gameObject.SetActive(true);
            }
        }
    }

    public void CompletedClimb()
    {
        if (InterviewManager.Instance != null)
        {
            if (InterviewManager.Instance.isAutoplay)
            {
                highScore.text = "Time survived: " + Stopwatch.timeElapsed.ToString("F2");
                Time.timeScale = 0f;
                completedPopUp.SetActive(true);
                next.onClick.AddListener(InterviewManager.Instance.NextStage);
                back.onClick.AddListener(GoToMainMenu);
                InterviewManager.Instance.climbingGrade = Math.Min(1, Stopwatch.timeElapsed / goalTime);
                return;
            }
        }
        // calculate levels
        int levelsGained = Mathf.RoundToInt(Stopwatch.timeElapsed) / 10;   // 1 level per 10 seconds survived? we'll figure it out
        int currentLevel = PlayerPrefs.GetInt("climbing_skill", 1);
        PlayerPrefs.SetInt("climbing_skill", currentLevel + levelsGained);  // might be able to be moved to a more generic script
        highScore.text = "High Score: " + levelsGained + "\nClimbing level: " + currentLevel + " -> " + (currentLevel + levelsGained);
        Time.timeScale = 0f;
        completedPopUp.SetActive(true);
        back.onClick.AddListener(GoToMainMenu); //goes back to the selection area
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Overworld");
    }

}
