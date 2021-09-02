using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Road_Enemy : MonoBehaviour
{
    /// <summary>
    /// A class that holds the information for each wave of enemies that run along the road
    /// </summary>
    [System.Serializable]
    public class Wave
    {
        //might change to lists in order to have multiple types of enemy for waves
        public GameObject enemyPrefab;
        public float spawnInterval = 2;
        public int maxEnemies;
    }
    /// <summary>
    /// The waypoints along the track
    /// </summary>
    public GameObject[] waypoints;//potentally convert into a list of arrays in order to have multiple routes
    /// <summary>
    /// The total waves for the level
    /// </summary>
    public Wave[] waves;
    /// <summary>
    /// The ammount of time between waves
    /// </summary>
    public int timeBetweenWaves = 5;
    /// <summary>
    /// The game manager for the level
    /// </summary>
    private Game_Manager gameManager;
    /// <summary>
    /// The time that the last enemy spawned
    /// </summary>
    private float lastSpawnTime;
    /// <summary>
    /// The total number of eneimes spawned durring the wave
    /// </summary>
    private int enemiesSpawned = 0;

    private void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
    }
    private void Update()
    {
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;

            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].maxEnemies)
            {
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject)
                    Instantiate(waves[currentWave].enemyPrefab,waypoints[0].transform);
                newEnemy.GetComponent<Road_Enemy_AI>().waypoints = waypoints;
                enemiesSpawned++;
            }
        }

        if(enemiesSpawned == waves[currentWave].maxEnemies &&
            GameObject.FindGameObjectWithTag("Runner") == null &&
            GameObject.FindGameObjectWithTag("Breaker") == null)
        {
            gameManager.Wave++;
            gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
            enemiesSpawned = 0;
            lastSpawnTime = Time.time;
        }
        else
        {
            gameManager.gameOver = true;
        }
    }
}
