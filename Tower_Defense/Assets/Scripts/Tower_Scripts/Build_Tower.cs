//This script is a modified version of the code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:2/23/23
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Build_Tower : MonoBehaviour
{
    /// <summary>
    /// The tower to try building
    /// </summary>
    public GameObject towerPrefab;
    private GameObject tower;
    /// <summary>
    /// The marker to turn off when placing the tower 
    /// </summary>
    public GameObject marker;
    /// <summary>
    /// The game manager used to access the players gold
    /// </summary>
    private Game_Manager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
    }
    public void build()
    {
        if(gameManager.Gold >= towerPrefab.GetComponentInChildren<Tower_Data>().levels[0].cost)
        {
            tower = (GameObject)
               Instantiate(towerPrefab,
               marker.transform.position,
               Quaternion.identity);
            gameManager.Gold -= tower.GetComponentInChildren<Tower_Data>().CurrentLevel.cost;
            //Get the physical boundries of the tower and updates the pathfinding with those boundries
            AstarPath.active.UpdateGraphs(tower.transform.Find("Hitbox").GetComponent<BoxCollider2D>().bounds);
            foreach(GameObject breaker in GameObject.FindGameObjectsWithTag("Breaker"))
            {
                try
                {
                    breaker.GetComponent<Breaker_Enemy_AI>().retarget();
                }
                catch
                {
                    //do nothing enemy dead
                }
            }
            Destroy(marker);
        }
    }
}
