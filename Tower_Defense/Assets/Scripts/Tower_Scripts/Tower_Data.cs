//This script is a modified version of the placemonster code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:8/25/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used for storing data on the current level of the tower
/// </summary>
[System.Serializable]
public class Tower_Level 
{
    //[System.Serializable] allows the class to be edited in the inspector
    /// <summary>
    /// The cost of the level of the tower
    /// </summary>
    public int cost;
    /// <summary>
    /// The way the tower looks
    /// </summary>
    public GameObject visual;
}


public class Tower_Data : MonoBehaviour
{
    /// <summary>
    /// A list containing the levels of the tower
    /// </summary>
    public List<Tower_Level> levels;
    /// <summary>
    /// The current level of the tower
    /// </summary>
    private Tower_Level currentLevel;
    /// <summary>
    /// a publicly accessible version of the level of the tower
    /// </summary>
    public Tower_Level CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            //set value of currentLevel
            currentLevel = value;
            //get index of new level
            int currentLevelIndex = levels.IndexOf(currentLevel);
            //get the correct visual
            GameObject levelVisuals = levels[currentLevelIndex].visual;
            //turns on the correct visual and turns off the remaining ones
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisuals != null)
                {
                    if (i == currentLevelIndex)
                    {
                        levels[i].visual.SetActive(true);
                    }
                    else
                    {
                        levels[i].visual.SetActive(false);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Figures out the next level of the tower if there is one
    /// </summary>
    /// <returns>Returns the next tower level or null if tower is max level</returns>
    public Tower_Level getNextLevel()
    {
        //where the current level is in the list of levels
        int currentLevelIndex = levels.IndexOf(currentLevel);
        //the tower level that is the highest
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// Increases the level of the tower if it is able to
    /// </summary>
    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }

    public void OnEnable()
    {
        CurrentLevel = levels[0];
    }

}
