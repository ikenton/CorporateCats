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
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMiceCountText("Mice Killed: ");
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
    /*void LevelUp(int  level)
    {
        miceCount = 0;
        Time.timeScale = 0f; //pauses the game
        hitText.gameObject.SetActive(false);
        levelPopUp.SetActive(true);
        levelNum++;
        levelUpTextNum.text = "Level " + levelNum;
        back.onClick.AddListener(GoToMainMenu); //goes back to the selection area
        playAgain.onClick.AddListener(Reload);
    }*/
    public void CompletedPounce()
    {
        highScore.text = "High Score: " + miceCount;
        Time.timeScale = 0f;
        hitText.gameObject.SetActive(false);
        completedPopUp.SetActive(true);    
        back.onClick.AddListener(GoToMainMenu); //goes back to the selection area
    }
    void GoToMainMenu()
    {
        Debug.Log("Go to main menu");
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
