using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverworldUIController : MonoBehaviour
{
    public TextMeshProUGUI skills;

    // Start is called before the first frame update
    void Start()
    {
        skills.SetText(
            "Skills: \n" +
            "Pouncing: " + PlayerPrefs.GetInt("pouncing_skill", 1) + "\n" +
            "Climbing: " + PlayerPrefs.GetInt("climbing_skill", 1) + "\n" +
            "Baking: " + PlayerPrefs.GetInt("baking_skill", 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
