using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ResultsController : MonoBehaviour
{
    public GameObject ui;

    public TMP_Text pouncingText;
    public TMP_Text climbingText;
    public TMP_Text bakingText;
    public TMP_Text overallText;

    public Button resultsButton;
    public VideoPlayer fail;
    public VideoPlayer pass;


    // Start is called before the first frame update
    void Start()
    {
        string pouncingGrade = (InterviewManager.Instance.pouncingGrade*100).ToString("F2");
        string climbingGrade = (InterviewManager.Instance.climbingGrade*100).ToString("F2");
        string bakingGrade = (InterviewManager.Instance.bakingGrade*100).ToString("F2");
        string overall = ((InterviewManager.Instance.pouncingGrade + InterviewManager.Instance.climbingGrade) / 2 * 100).ToString("F2");

        pouncingText.text = "Pouncing: " + pouncingGrade + "%";
        climbingText.text = "Climbing: " + climbingGrade + "%";
        bakingText.text = "Baking: " + bakingGrade + "%";
        overallText.text = "Overall: " + overall + "%";

        float failTime = (float)fail.length;
        float passTime = (float)pass.length;


        resultsButton.onClick.AddListener(() =>
        {
            PlayResultsCutscene();
        });
    }

    void PlayResultsCutscene()
    {
        Debug.Log("Playing results cutscene");
        if ((InterviewManager.Instance.pouncingGrade + InterviewManager.Instance.climbingGrade) / 2 > 0.7f)
        {
            ui.SetActive(false);
            pass.Play();
            Invoke("BackToMain", (float)pass.length);
        }
        else
        {
            ui.SetActive(false);
            fail.Play();
            Invoke("BackToMain", (float)fail.length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BackToMain()
    {
        SceneManager.LoadScene("Overworld");
    }
}
