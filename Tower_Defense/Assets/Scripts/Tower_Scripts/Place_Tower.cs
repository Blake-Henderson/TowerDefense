//This script is a modified version of the code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:10/13/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place_Tower : MonoBehaviour
{
    /// <summary>
    /// this is the prefab of the tower to be placed
    /// </summary>
    public GameObject towerPrefab;
    /// <summary>
    /// Used to see if a tower is currently in a location
    /// </summary>
    private GameObject tower;
    /// <summary>
    /// The game manager used to access the players gold
    /// </summary>
    private Game_Manager gameManager;
    /// <summary>
    /// Mainly used to see if the marker needs to be redrawn
    /// </summary>
    private void FixedUpdate()
    {
        if (tower == null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    /// <summary>
    /// activates when the player clicks on a tower marker
    /// </summary>
    private void OnMouseUp()
    {
        //creates a tower of the towerPrefab at the location of the marker and makes it the 
        //child of the marker.This is done to make turning the sprite on and off easier
        //among other things
        if (CanPlaceTower())
        {
            tower = (GameObject)
                Instantiate(towerPrefab,
                transform.position,
                Quaternion.identity, gameObject.transform);
            gameManager.Gold -= tower.GetComponent<Tower_Data>().CurrentLevel.cost;
        }
        else if (canUpgradeTower())
        {
            tower.GetComponent<Tower_Data>().IncreaseLevel();
            gameManager.Gold -= tower.GetComponent<Tower_Data>().CurrentLevel.cost;
        }
    }
    /// <summary>
    /// called when the game tower spawn locations are spawned
    /// </summary>
    private void Start()
    {
        //finds the empty game object named Game Manager then selects its Game_Manager script that's attached
        gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
    }
    /// <summary>
    /// A simple function that sees if the tower game object is emtpy.
    /// </summary>
    /// <returns>a bool if you can place a tower or not</returns>
    private bool CanPlaceTower()
    {
        int cost = towerPrefab.GetComponent<Tower_Data>().levels[0].cost;
        return tower == null && gameManager.Gold >= cost;
    }
    /// <summary>
    /// Determines if a tower is upgradable
    /// </summary>
    /// <returns>true if the tower can be upgraded and false if not</returns>
    private bool canUpgradeTower()
    {
        if (towerPrefab != null)
        {
            Tower_Data towerData = towerPrefab.GetComponent<Tower_Data>();
            Tower_Level nextLevel = towerData.getNextLevel();
            if (nextLevel != null)
            {
                //Debug.Log(nextLevel.cost);
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }
}
