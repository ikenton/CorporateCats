using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySlider : MonoBehaviour
{
    public Image greenArea;
    public Image slider;
    public Image qteBar;
    public TextMeshProUGUI numOfMice;
    public RectTransform rt;
    public RectTransform sliderRT;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI highScore;
    public BoxCollider2D greenBoxCollider;
    public Transform cat;
    public Transform mouse;
    public GameObject completedPopUp;
    public Button back;
    public Button next;
    public GameObject interviewUI;
    public static float widthOfBar = 410f;
    public float speed = 0.5f;
    public bool hit = false;
    public int miceCount = 0;
    public bool visible = false;
    public float moveDuration = 3f;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private Vector3 currentPosition;
    public Vector3 temp;
    public Animator animator;
    public bool isAutoplay = false;
    public int initialPlayerLevel = 15;
    // dictates what skill level the player needs to be at to guarantee hits when autoplaying
    public int autoplaySkillLevel = 20;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCor());
        startPosition = new Vector3(-202.4f, slider.rectTransform.localPosition.y, slider.rectTransform.localPosition.z);
        endPosition = new Vector3(startPosition.x+404.8f, startPosition.y, 0f);
        OffsetGreenArea();
        UpdateMiceCountText("Mice Killed: ");
        ChangeDifficulty();
        completedPopUp.SetActive(false);
        initialPlayerLevel = PlayerPrefs.GetInt("pouncing_skill", 1);

        isAutoplay = InterviewManager.Instance.isAutoplay;
        interviewUI.SetActive(isAutoplay);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMiceCountText("Mice Killed: ");
        // autoplay is enabled
        // TODO: make this not bad LOL
        // Right now, this just makes the compuer win every time
        // In the future, I want this to be based off of the player's skill level
        // Higher skill levels will have a higher chance of winning
        // This is to make it compatible for the "interview" mode


        if (hit &&  cat.transform.position.x != 2.75f) //if cat has not pounced then move
        {
            MoveCat();
        }
        ManageBar();
    }

    public void HandleHit()
    {
        Debug.Log("ran");
        hit = true;
        DisplayHitText("HIT!");
        MoveCat();
        miceCount++;
        UpdateMiceCountText("Mice Killed: ");
        ChangeDifficulty();
    }

    void OffsetGreenArea()
    {
        rt = greenArea.GetComponent<RectTransform>();
        int min = -117;
        int max = 119;

        float positionX = RandomNumberGenerator.GetInt32(min, max);
        rt.anchoredPosition = new Vector2(positionX, 43f);
        
    }

    public void ChangeDifficulty()
    {
        rt = greenArea.GetComponent<RectTransform>();
        greenBoxCollider = rt.GetComponent<BoxCollider2D>();

        rt.sizeDelta = new Vector2(142f, 86f);
        //adjust green bar size & size
        speed += 0.05f;
        
    }
    public void UpdateMiceCountText(string text)
    {
        numOfMice.text = "Mice killed: " + miceCount;
    }
    public void MoveCat()
    {
        //TODO: make the cat move in the y direction so that it looks like its actually jumping and not just sliding
        if (hit && cat.transform.position.x != mouse.transform.position.x)
        {
            animator.SetFloat("speed", 1);  // temp animation
            cat.transform.Translate(Vector3.right * 10f * Time.deltaTime);
            
        }
        
        if (cat.transform.position.x >= mouse.transform.position.x) //stop cat @ mouse position
        {
            animator.SetFloat("speed", 0);
            cat.transform.position = new Vector3(mouse.transform.position.x, cat.transform.position.y, cat.transform.position.z);
            
        }

    }
    void ManageBar()
    {
        if (!hit)
        {
            if (MovingBar.enter && Input.GetButtonDown("Jump"))
            {
                //if hit
                Debug.Log("HIT");
                hit = true;
                DisplayHitText("HIT!");
                MoveCat();
               
                miceCount++;
                UpdateMiceCountText("Mice Killed: ");

                ChangeDifficulty(); // make game harder with every hit
                
            }
            else if(!MovingBar.enter && Input.GetButtonDown("Jump"))
            {
                CompletedPounce();
            }
            
        }
        if (cat.transform.position.x == mouse.transform.position.x && hit)
        {
            //Debug.Log("ON IT");
            ResetLevel();
        }
    }
    
    IEnumerator MoveCor()
    {
        float progress = 0;
        currentPosition = startPosition;
        while (progress < 1)
        {
            progress += Time.deltaTime * speed;
            if (!hit)
            {
                currentPosition = Vector3.Lerp(startPosition, endPosition, progress);
                slider.rectTransform.localPosition = currentPosition;
            }
            

            yield return null;
        }

        currentPosition = endPosition;
        if(currentPosition == endPosition)
        {
            
            temp = startPosition; 
            startPosition = endPosition;
            endPosition = temp;
            StartCoroutine(MoveCor());
            //Move();//recursively calls it so it will move back and forth
        }
    }

    IEnumerator DisplayHitTextCor(string hit)
    {
        hitText.text = hit;
        hitText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        hitText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        visible = true;


    }
    public void DisplayHitText(string hit)
    {
        hitText.text = hit;
        StartCoroutine(DisplayHitTextCor(hit));
        //hitText.gameObject.SetActive(true);
    }
    
    public void CompletedPounce()
    {
        // calculate levels
        if (!isAutoplay)
        {
            int currentLevel = PlayerPrefs.GetInt("pouncing_skill", 1);
            int levelsGained = miceCount / 5;   // temp, will be changed to a more complex formula later... maybe
            PlayerPrefs.SetInt("pouncing_skill", currentLevel + levelsGained);  // might be able to be moved to a more generic script
            highScore.text = "High Score: " + miceCount + "\nPouncing level: " + currentLevel + " -> " + (currentLevel + levelsGained);
        }
        else if (isAutoplay)
        {
            highScore.text = "Mice slain: " + miceCount;
            next.gameObject.SetActive(true);
            // calculate grade
            InterviewManager.Instance.pouncingGrade = Math.Min(1, miceCount / interviewUI.GetComponent<Timer>().goal);
            next.onClick.AddListener(InterviewManager.Instance.NextStage);
        }
        Time.timeScale = 0f;
        hitText.gameObject.SetActive(false);
        completedPopUp.SetActive(true);    
        back.onClick.AddListener(GoToMainMenu); //goes back to the selection area
    }
    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Overworld");

    }
    void ResetLevel() //called after a mouse is killed
    {
        Time.timeScale = 0f;
        startPosition = new Vector3(-202.4f, slider.rectTransform.localPosition.y, slider.rectTransform.localPosition.z);
        endPosition = new Vector3(startPosition.x + 404.8f, startPosition.y, 0f);
        slider.transform.localPosition = startPosition;
        Debug.Log("resetting");
        cat.transform.position = new Vector3(-3.9f, cat.transform.position.y, cat.transform.position.z);
        CatPounce.pounced = false;
        hit = false;
        ManageBar();//reset the sliding bar
        OffsetGreenArea(); //change the greenarea
        ChangeDifficulty();//change the difficulty if necessary
        Time.timeScale = 1f;


    }

}
