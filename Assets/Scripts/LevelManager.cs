using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
   
    public MySlider pouncingGame;
    public ClimbUIController climbController;
    public BowlBeingFilled bakingGame; //need to get the score from baking script.
    private int currentLevel;
    private int levelsGained;

   
    public int CalculatePounceLvl()
    {
        return pouncingGame.miceCount / 5;
    }

    public int CalculateClimbLvl()
    {

        return Mathf.RoundToInt(Stopwatch.timeElapsed) / 10;   // 1 level per 10 seconds survived? we'll figure it out

    }
    public int CalculateBakingLvl()
    {
        return 0; //replace 0 with the number of biscuits made.
    }

    public void UpdateLevels()
    {
        if(pouncingGame != null)
        {
            currentLevel = PlayerPrefs.GetInt("pouncing_skill", 1);
            levelsGained = CalculatePounceLvl();
            PlayerPrefs.SetInt("pouncing_skill", currentLevel + levelsGained);  // might be able to be moved to a more generic script

        }

        if(climbController != null) 
        {
            currentLevel = PlayerPrefs.GetInt("climbing_skill", 1);
            levelsGained = CalculateClimbLvl();
            PlayerPrefs.SetInt("climbing_skill", currentLevel + levelsGained);

        }

        if(bakingGame != null)
        {
            currentLevel = PlayerPrefs.GetInt("baking_skill", 1);
            levelsGained = CalculateClimbLvl();
            PlayerPrefs.SetInt("baking_skill", currentLevel+levelsGained);
        }
        SceneManager.LoadScene("Overworld");

    }
}
