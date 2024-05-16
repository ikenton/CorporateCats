using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InterviewManager : MonoBehaviour
{
    public static InterviewManager Instance;
    public bool isAutoplay;
    public string[] levels = new string[]
    {
        "QuickTimeEvent",
        "ClimbingEvent",
        "BakingEvent",
    };

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
        for (int i = 0; i < levels.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == levels[i])
            {
                if (i == levels.Length - 1)
                {
                    SceneManager.LoadScene("Overworld");    // temp, move to final scene later
                }
                else
                {
                    SceneManager.LoadScene(levels[i + 1]);
                }
            }
        }
    }   
}
