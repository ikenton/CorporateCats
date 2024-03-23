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
    public TextMeshProUGUI level;
    public TextMeshProUGUI numOfMice;
    public RectTransform rt;
    public RectTransform sliderRT;
    public TextMeshProUGUI hitText;
    public BoxCollider2D greenBoxCollider;
    public Transform cat;
    public Transform mouse;
    public GameObject levelPopUp;
    public TextMeshProUGUI levelUpText;
    public Button back;
    public Button playAgain;
    public TextMeshProUGUI levelUpTextNum;
    public GameObject completedPopUp;
    public static float widthOfBar = 410f;
    public int levelNum = 1;
    public float speed = 0.5f;
    public bool hit = false;
    public int miceCount = 0;
    public bool visible = false;
    public float moveDuration = 3f;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private Vector3 currentPosition;
    public Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCor());

        startPosition = new Vector3(-202.4f, slider.rectTransform.localPosition.y, slider.rectTransform.localPosition.z);
        endPosition = new Vector3(startPosition.x+404.8f, startPosition.y, 0f);
        OffsetGreenArea();
        UpdateLevelText("Level ");
        UpdateMiceCountText("Mice Killed: ");
        ChangeDifficulty();
        levelPopUp.SetActive(false);
        completedPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMiceCountText("Mice Killed: ");
        UpdateLevelText("Level ");
        if (hit &&  cat.transform.position.x != 2.75f) //if cat has not pounced then move
        {
            MoveCat();
        }
        ManageBar();
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
        
        switch (levelNum)
        {
            case 1:
                rt.sizeDelta = new Vector2(142f, 86f);
                greenBoxCollider.size = new Vector2(142f, 86f);
                speed = 0.5f;
                break;
            case 2:
                rt.sizeDelta = new Vector2(100f, 86f);
                greenBoxCollider.size = new Vector2(100f, 86f);
                speed = 0.7f;
                break;
            case 3:
                rt.sizeDelta = new Vector2(80f, 86f);
                greenBoxCollider.size = new Vector2(80f, 86f);
                speed =  0.9f;
                break;
            case 4:
                rt.sizeDelta = new Vector2(50f, 86f);
                greenBoxCollider.size = new Vector2(50f, 86f);
                speed = 1f;
                break;
            case 5:
                rt.sizeDelta = new Vector2(25f, 86f);
                greenBoxCollider.size = new Vector2(25f, 86f);
                speed = 1.5f;

                break;
        }
    }

    public void UpdateLevelText(string text)
    {
        level.text = "Level " + levelNum;
        levelUpTextNum.text = "Level " + levelNum;
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
            cat.transform.Translate(Vector3.right * 10f * Time.deltaTime);
            
        }
        
        if (cat.transform.position.x >= mouse.transform.position.x) //stop cat @ mouse position
        {
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
                
            }
            else if(!MovingBar.enter && Input.GetButtonDown("Jump"))
            {
                DisplayHitText("MISSED");
            }
            
        }
        if (cat.transform.position.x == mouse.transform.position.x && hit)
        {
            //Debug.Log("ON IT");
            ResetLevel(miceCount, levelNum);
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
    void LevelUp(int  level)
    {
        miceCount = 0;
        Time.timeScale = 0f; //pauses the game
        hitText.gameObject.SetActive(false);
        levelPopUp.SetActive(true);
        levelNum++;
        levelUpTextNum.text = "Level " + levelNum;
        back.onClick.AddListener(GoToMainMenu); //goes back to the selection area
        playAgain.onClick.AddListener(Reload);
    }
    public void CompletedPounce()
    {
        Time.timeScale = 0f;
        hitText.gameObject.SetActive(false);
        completedPopUp.SetActive(true);    
        back.onClick.AddListener(GoToMainMenu); //goes back to the selection area
    }
    void GoToMainMenu()
    {
        Debug.Log("Go to main menu");
        levelPopUp.SetActive(false);
    }
    void Reload()
    {
        Time.timeScale = 1f;
        levelPopUp.SetActive(false);
        ResetLevel(miceCount, levelNum);
    }
    void ResetLevel(int miceKilled, int levelNum) //called after a mouse is killed
    {
        //BriefPause(0.5f);
        //hitText.gameObject.SetActive(false);
        //BriefPause(1f);
        hit = false;
        
        if (miceKilled >= 3 && levelNum < 5)
        {
            LevelUp(levelNum);
        }
        else if(miceKilled >= 3 && levelNum == 5)
        {
            CompletedPounce();
        }
        else
        {
            Debug.Log("resetting");
            cat.transform.position = new Vector3(-3.9f, cat.transform.position.y, cat.transform.position.z);
            CatPounce.pounced = false;
            ManageBar();//reset the sliding bar
            OffsetGreenArea(); //change the greenarea
            ChangeDifficulty();//change the difficulty if necessary
        }
      
    }
    public void BriefPause(float seconds)
    {
        StartCoroutine(BriefPauseCor(seconds));
        //return true;
    }
    public IEnumerator BriefPauseCor(float seconds)
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + seconds;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        
    }

}
