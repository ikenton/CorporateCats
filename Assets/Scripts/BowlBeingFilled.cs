using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BowlBeingFilled : MonoBehaviour
{
     public TextMeshProUGUI highScore;

    [SerializeField] private AudioClip dropClip, finish;
    public GameObject bowl;
    public GameObject[] ingredients;
    private List<GameObject> ingredientOrder = new List<GameObject>();
    public GameObject biscuit;
    public int x;
    public int biscuitCount;
    public Image[] orderDisplaySlots;
    public Vector3[] initialPositions;
    public TextMeshProUGUI numOfBiscuits;
    public GameObject completedPopUp;
    public Button back;
    public int currentPlayerLevel;
    public int levelsGained;
    void Start()
    {
        x = 0;
        currentPlayerLevel = PlayerPrefs.GetInt("baking_skill", 1);
        completedPopUp.SetActive(false);
        ShuffleIngredients(ingredients);
        foreach (GameObject ingredient in ingredients)
        {
            ingredientOrder.Add(ingredient);
        }
        DisplayOrder();
        Debug.Log(ingredientOrder[0]);
        Debug.Log(x);
        SaveInitialPositions();
        biscuit.GetComponent<SpriteRenderer>().enabled = false;
        biscuitCount = 0;
        levelsGained = 0; 
    }

    void ShuffleIngredients(GameObject[] ingredientsList)
    {
        for (int i = 0; i < ingredientsList.Length; i++)
        {
            int random = UnityEngine.Random.Range(i, ingredientsList.Length);
            GameObject temp = ingredientsList[random];
            ingredientsList[random] = ingredientsList[i];
            ingredientsList[i] = temp;
        }
    }
  
    void DisplayOrder()
    {
        for (int i = 0; i < orderDisplaySlots.Length; i++)
        {
            if (i < ingredientOrder.Count)
            {
                orderDisplaySlots[i].sprite = ingredientOrder[i].GetComponent<SpriteRenderer>().sprite;
                orderDisplaySlots[i].gameObject.SetActive(true); 
            }
            else
            {
                orderDisplaySlots[i].gameObject.SetActive(false);
            }
        }
    }
    void SaveInitialPositions()
    {
        initialPositions = new Vector3[ingredients.Length];
        for (int i = 0; i < ingredients.Length; i++)
        {
            initialPositions[i] = ingredients[i].transform.position;
        }
    }
    void UpdateBiscuitCount()
    {
        biscuitCount++;
        numOfBiscuits.text = biscuitCount.ToString();

    }

   void ResetGame()
    {
        StartCoroutine(ResetWithDelay(1f));
    }

    IEnumerator ResetWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        x = 0;
        ingredientOrder.Clear();
        ShuffleIngredients(ingredients);
        foreach (GameObject ingredient in ingredients)
        {
            ingredientOrder.Add(ingredient);
        }
        DisplayOrder(); 

        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].transform.position = initialPositions[i];
        }
        foreach (GameObject ingredient in ingredients)
        {
            ingredient.GetComponent<SpriteRenderer>().enabled = true;
        }
        bowl.GetComponent<SpriteRenderer>().enabled = true;
        biscuit.GetComponent<SpriteRenderer>().enabled = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("TRIGGERED.");
        if (col.gameObject == ingredientOrder[x])
        {
            x++;
            Debug.Log("Touched.");
            col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            AudioSource.PlayClipAtPoint(dropClip, transform.position);

            if (x==3)
            {
                Transform bowlTransform = this.gameObject.GetComponent<Transform>();
                bowlTransform.localScale = new Vector3(12f, 12f, 1f);
                biscuit.GetComponent<SpriteRenderer>().enabled = true;
                AudioSource.PlayClipAtPoint(finish, transform.position);
                UpdateBiscuitCount();
                ResetGame();
            }
        }
        else
        {
            // game over!
            levelsGained = (int)Math.Floor((double)biscuitCount / 2);
            Debug.Log("You lose!");
            completedPopUp.SetActive(true);
            back.onClick.AddListener(GoToMainMenu);
            PlayerPrefs.SetInt("pouncing_skill", currentPlayerLevel + levelsGained);
            highScore.text = "High Score: " + biscuitCount + "\nPouncing level: " + currentPlayerLevel + " -> " + (currentPlayerLevel + levelsGained);
        }
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Overworld");

    }
}

