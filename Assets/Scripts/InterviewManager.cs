using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InterviewManager : MonoBehaviour
{
    public static InterviewManager Instance;
    public bool isAutoplay;
    public string[] levels = new string[] { };

    // The plauer's performance in each skill
    // Will be calculated into a final grade that determines passing or failing
    public float pouncingGrade = 0.00f;
    public float climbingGrade = 0.00f;
    public float bakingGrade = 0.00f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStage()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < levels.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == levels[i])
            {
                if (i == levels.Length - 1)
                {
                    SceneManager.LoadScene("InterviewResults");    // temp, move to final scene later
                }
                else
                {
                    SceneManager.LoadScene(levels[i + 1]);
                }
            }
        }
    }   
}
