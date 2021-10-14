//Author:Blake Henderson
//Date:10/13/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            marker.SetActive(false);
        }
    }
}
