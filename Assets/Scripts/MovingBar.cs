using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBar : MonoBehaviour
{
    public static bool enter = false;
    public GameObject cat;
    public GameObject mouse;
    public GameObject uiManager;
    public MySlider script;
    public int chanceOfHit = 0;

    private void Start()
    {
        script = uiManager.GetComponent<MySlider>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        enter = true;
        if (script.isAutoplay)
        {
            int randomNumber = UnityEngine.Random.Range(0, 100);
            double autoplayChance = (Convert.ToDouble(script.initialPlayerLevel) / Convert.ToDouble(script.autoplaySkillLevel)) * 100.00;
            Debug.Log("autoplayChance: " + autoplayChance);
            Debug.Log("randomNumber: " + randomNumber);
            Debug.Log(script.initialPlayerLevel);
            Debug.Log(script.autoplaySkillLevel);
            if (randomNumber <= autoplayChance)
            {
                uiManager.SendMessage("HandleHit");
            } 
            else
            {
                Debug.Log("miss");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("stay");
      
        
        //Debug.Log(enter);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        enter = false;
        //Debug.Log("exit");
        //Debug.Log(enter);
    }

   
}
