//This script is a modified version of the code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:8/29/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Enemy_AI : MonoBehaviour
{
    /// <summary>
    /// An array of all the waypoints on the road
    /// </summary>
    [HideInInspector]
    public GameObject[] waypoints;

    public Health_Bar health;
    /// <summary>
    /// The waypoint the enemy is walking away from
    /// </summary>
    private int currentWaypoint = 0;
    /// <summary>
    /// The time in which the enemy passes over the current waypoint
    /// </summary>
    public float lastWaypointSwitchTime;
    /// <summary>
    /// The speed of the enemy
    /// </summary>
    private float speed = 1.0f;

    private void Start()
    {
        lastWaypointSwitchTime = Time.time;
        speed = gameObject.GetComponent<Enemy_Data>().speed;
    }
    public void init()
    {
        lastWaypointSwitchTime = Time.time;
        speed = gameObject.GetComponent<Enemy_Data>().speed;
        health.currentHealth = health.maxHealth;
        currentWaypoint = 0;
        SpriteRenderer sprite = gameObject.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        sprite.flipX = false;
        if(gameObject.TryGetComponent(out Rainbow_Slime_Coloring slime))
        {
            slime.changeColor();
        }
    }
    private void Update()
    {
        //the way point the enemy is currently on
        Vector3 startPosition = new Vector3(waypoints[currentWaypoint].transform.position.x,
            waypoints[currentWaypoint].transform.position.y, gameObject.transform.position.z);
        //the next waypoint down the track
        Vector3 endPosition = new Vector3 (waypoints[currentWaypoint + 1].transform.position.x, 
            waypoints[currentWaypoint + 1].transform.position.y, gameObject.transform.position.z);

        //the distance between the two waypoints
        float pathLength = Vector3.Distance(startPosition, endPosition);
        //determines how long it will take the enemy to move between the two positions
        float totalTimeForPath = pathLength / speed;
        //how long the enemy has been on the path
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        //.Lerp does math to determine where the enemy should be
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        //swaps to next waypoint or does damage to the player depending on where the enemy is
        if (gameObject.transform.position.Equals(endPosition))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {

                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                RotateIntoMoveDirection();
            }
            else
            {

                Destroy(gameObject);

                Game_Manager gameManager =
                    GameObject.Find("Game Manager").GetComponent<Game_Manager>();
                if (gameManager.Lives > 0)
                {
                    gameManager.Lives -= 1;
                }
            }
        }

    }
    /// <summary>
    /// Rotates the enemy to the left or right depending on which direction they are going
    /// </summary>
    private void RotateIntoMoveDirection()
    {
            Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
            Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
            Vector3 newDirection = (newEndPosition - newStartPosition);
            float x = newDirection.x;
            SpriteRenderer sprite = gameObject.transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
            if (x < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
    }

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(
            gameObject.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }
}
