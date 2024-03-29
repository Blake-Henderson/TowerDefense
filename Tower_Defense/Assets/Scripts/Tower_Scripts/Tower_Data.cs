//This script is a modified version of the code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:2/23/23
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
    /// <summary>
    /// The projectile the tower fires
    /// </summary>
    public GameObject bullet;
    /// <summary>
    /// The damage of the tower
    /// </summary>
    public int damage;
    /// <summary>
    /// The fire rate of the tower
    /// </summary>
    public float fireRate;
    /// <summary>
    /// The range of the tower
    /// </summary>
    public float range = 3.5f;
    /// <summary>
    /// The max health of the tower
    /// </summary>
    public int maxHealth = 100;
}


public class Tower_Data : MonoBehaviour
{
    /// <summary>
    /// A list containing the levels of the tower
    /// </summary>
    public List<Tower_Level> levels;
    /// <summary>
    /// The index of the current level
    /// </summary>
    public int currentLevelIndex = 0;
    /// <summary>
    /// The name of the type of tower
    /// </summary>
    public string towerName = "Tower";
    /// <summary>
    /// The health of the tower
    /// </summary>
    public Health_Bar health;
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
    private Game_Manager gameManager;
    /// <summary>
    /// Figures out the next level of the tower if there is one
    /// </summary>
    /// <returns>Returns the next tower level or null if tower is max level</returns>
    public Tower_Level getNextLevel()
    {
        //Debug.Log(currentLevelIndex);
        //the tower level that is the highest
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            // Debug.Log("getNextLevel currentLevel" + currentLevelIndex);
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
        if (currentLevelIndex < levels.Count - 1 && gameManager.Gold >= levels[currentLevelIndex + 1].cost)
        {
            //Debug.Log("preIncreaseLevel" + currentLevelIndex);
            currentLevelIndex += 1;
            // Debug.Log("postIncreaseLevel" + currentLevelIndex);
            CurrentLevel = levels[currentLevelIndex];
            gameObject.GetComponent<CircleCollider2D>().radius = currentLevel.range;
            gameManager.Gold -= levels[currentLevelIndex].cost;
            health.maxHealth = levels[currentLevelIndex].maxHealth;
            health.currentHealth = health.maxHealth;
        }
    }

    public void OnEnable()
    {
        CurrentLevel = levels[0];
        gameObject.GetComponent<CircleCollider2D>().radius = levels[0].range;
        gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
        health.maxHealth = levels[0].maxHealth;
        health.currentHealth = health.maxHealth;
    }
}
