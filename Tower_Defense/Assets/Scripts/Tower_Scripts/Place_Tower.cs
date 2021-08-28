//This script is a modified version of the placemonster code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:8/25/21
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
    /// Mainly used to see if the marker needs to be redrawn
    /// </summary>
    private void FixedUpdate()
    {
        if (CanPlaceTower())
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
        tower = (GameObject)
            Instantiate(towerPrefab,
            transform.position,
            Quaternion.identity,gameObject.transform);


    }
    /// <summary>
    /// A simple function that sees if the tower game object is emtpy.
    /// </summary>
    /// <returns>a bool if you can place a tower or not</returns>
    private bool CanPlaceTower()
    {
        return tower == null;
    }
}
