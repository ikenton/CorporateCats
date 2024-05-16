using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsController : MonoBehaviour
{
    public TMP_Text pouncingText;
    public TMP_Text climbingText;
    public TMP_Text bakingText;
    public TMP_Text overallText;

    public Button resultsButton;

    // Start is called before the first frame update
    void Start()
    {
        string pouncingGrade = (InterviewManager.Instance.pouncingGrade*100).ToString("F2");
        string climbingGrade = (InterviewManager.Instance.climbingGrade*100).ToString("F2");
        string bakingGrade = (InterviewManager.Instance.bakingGrade*100).ToString("F2");
        string overall = ((InterviewManager.Instance.pouncingGrade + InterviewManager.Instance.climbingGrade + InterviewManager.Instance.bakingGrade) / 3).ToString("F2");

        pouncingText.text = "Pouncing: " + pouncingGrade + "%";
        climbingText.text = "Climbing: " + climbingGrade + "%";
        bakingText.text = "Baking: " + bakingGrade + "%";
        overallText.text = "Overall: " + overall + "%";

        resultsButton.onClick.AddListener(() =>
        {
            PlayResultsCutscene();
        });
    }

    void PlayResultsCutscene()
    {
        Debug.Log("Playing results cutscene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
