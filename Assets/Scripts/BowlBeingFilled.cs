using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlBeingFilled : MonoBehaviour
{

    [SerializeField] private AudioClip dropClip, finish;
    public GameObject bowl;
    public GameObject[] ingredients;
    private List<GameObject> ingredientOrder = new List<GameObject>();
    public GameObject biscuit;
    public int x;
    public Image[] orderDisplaySlots;
    public Boolean correct = true;
    public Vector3[] initialPositions;
    private Vector3 bowlInitialPosition;
    void Start()
    {
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
                bowl.GetComponent<SpriteRenderer>().enabled = false;
                biscuit.GetComponent<SpriteRenderer>().enabled = true;
                AudioSource.PlayClipAtPoint(finish, transform.position);
                ResetGame();
            }
        }
        else
        {
            // game over!
            levelsGained = (int)Math.Floor((double)biscuitCount / 2);
            Debug.Log("You lose!");
        }
    }
}
