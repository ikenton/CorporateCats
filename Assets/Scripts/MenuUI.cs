using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button resume;
    public Button quit;
    public Button howTo;
    public Button overworld;
    public GameObject howToInstructions;
    public Button back;
    public GameObject popUp;
    // Start is called before the first frame update
    void Start()
    {
        popUp.SetActive(false);
        howToInstructions.SetActive(false);
        resume.onClick.AddListener(ResumeOnPressed);
        quit.onClick.AddListener(QuitOnPressed);
        howTo.onClick.AddListener(HowToPlayPressed);
        overworld.onClick.AddListener(OverworldOnPressed);
        back.onClick.AddListener(GoBack);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            popUp.SetActive(true);

        }
    }

    public void ResumeOnPressed()
    {
        popUp.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitOnPressed()
    {
        Application.Quit();
    }

    public void HowToPlayPressed()
    {
        howToInstructions.SetActive(true );
    }

    public void OverworldOnPressed()
    {
        Time.timeScale = 1.0f;
        popUp.SetActive(false);
        SceneManager.LoadScene("Overworld");

    }

    public void GoBack()
    {
        howToInstructions.SetActive(false);

    }

}
