using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BiscuitSlider : MonoBehaviour
{
    public TextMeshProUGUI numOfBiscuits;
    public TextMeshProUGUI bakingText;
    public TextMeshProUGUI highScore;
    public Transform egg;
    public Transform flour;
    public Transform milk;
    public GameObject completedPopUp;
    public Button back;
    public static float widthOfBar = 410f;
    public bool burnt = false;
    public int biscuitCount = 0;
    public bool visible = false;
    public float moveDuration = 3f;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private Vector3 currentPosition;
    public Vector3 temp;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        UpdateBiscuitCountText("Biscuits Baked: ");
        completedPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBiscuitCountText("Biscuits Baked: ");
    }


    // public void ChangeDifficulty()
    // {
    //     rt = greenArea.GetComponent<RectTransform>();
    //     greenBoxCollider = rt.GetComponent<BoxCollider2D>();

    //     rt.sizeDelta = new Vector2(142f, 86f);
    //     //adjust green bar size & size
    //     speed += 0.05f;
        
    // }

    public void UpdateBiscuitCountText(string text)
    {
        numOfBiscuits.text = "Biscuits baked: " + biscuitCount;
    }


    IEnumerator DisplaybakingTextCor(string burnt)
    {
        bakingText.text = burnt;
        bakingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        bakingText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        visible = true;


    }


    public void DisplaybakingText(string burnt)
    {
        bakingText.text = burnt;
        StartCoroutine(DisplaybakingTextCor(burnt));
        //bakingText.gameObject.SetActive(true);
    }
    /*void LevelUp(int  level)
    {
        biscuitCount = 0;
        Time.timeScale = 0f; //pauses the game
        bakingText.gameObject.SetActive(false);
        levelPopUp.SetActive(true);
        levelNum++;
        levelUpTextNum.text = "Level " + levelNum;
        back.onClick.AddListener(GoToMainMenu); //goes back to the selection area
        playAgain.onClick.AddListener(Reload);
    }*/

     public void CompletedPounce()
    {
        // calculate levels
        int levelsGained = biscuitCount;   // temp, will be changed to a more complex formula later... maybe
        int currentLevel = PlayerPrefs.GetInt("baking_skill", 1);
        PlayerPrefs.SetInt("baking_skill", currentLevel + levelsGained);  // might be able to be moved to a more generic script

        highScore.text = "High Score: " + biscuitCount + "\nBaking level: " + currentLevel + " -> " + (currentLevel + levelsGained);
        Time.timeScale = 0f;
        completedPopUp.SetActive(true);    
        back.onClick.AddListener(GoToMainMenu); //goes back to the selection area
    }
   
    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Overworld");

    }


}
