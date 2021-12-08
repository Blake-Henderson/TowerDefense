//This script is a modified version of the code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:12/5/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    /// <summary>
    /// A class that holds the information for each wave of enemies that run along the road
    /// </summary>
    [System.Serializable]
    public class Wave
    {
        /// <summary>
        /// each entry is an individual enemy prefab
        /// </summary>
        public List<Spawn_Data> enemies;
        public int reward;
    }
    /// <summary>
    /// The waves of enemies
    /// </summary>
    public Wave[] waves;
    /// <summary>
    /// The panel that holds the win text
    /// </summary>
    public GameObject winPanel;
    /// <summary>
    /// The game manager for the level
    /// </summary>
    private Game_Manager gameManager;
    /// <summary>
    /// The time that the last enemy spawned
    /// </summary>
    private float lastSpawnTime;
    /// <summary>
    /// The enemy the wave is currently on
    /// </summary>
    private int currentEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameOver)
        {
            int currentWave = gameManager.Wave;
            if (currentWave < waves.Length)
            {
                float timeInterval = Time.time - lastSpawnTime;
                if (currentEnemy < waves[currentWave].enemies.Count)
                {
                    float spawnInterval = waves[currentWave].enemies[currentEnemy].spawnTime;

                    if ((timeInterval > spawnInterval))
                    {
                        //spawn a runner enemy
                        if (waves[currentWave].enemies[currentEnemy].enemy.gameObject.tag.Equals("Runner"))
                        {
                            lastSpawnTime = Time.time;

                            GameObject newEnemy = (GameObject)SimplePool.Spawn(waves[currentWave].enemies[currentEnemy].enemy,
                                waves[currentWave].enemies[currentEnemy].path.waypoints[0].transform.position, Quaternion.identity);
                            newEnemy.GetComponent<Road_Enemy_AI>().init();
                            newEnemy.GetComponent<Road_Enemy_AI>().waypoints =
                                waves[currentWave].enemies[currentEnemy].path.waypoints;
                        }
                        //spawn a breaker enemey
                        else
                        {
                            //do nothing for now
                        }
                        currentEnemy++;
                    }
                }
            }
            if (currentWave >= waves.Length - 1)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else if (currentEnemy == waves[currentWave].enemies.Count &&
                GameObject.FindGameObjectWithTag("Runner") == null &&
                GameObject.FindGameObjectWithTag("Breaker") == null)
            {
                if (currentWave < waves.Length - 1)
                {
                    gameManager.Wave++;
                    gameManager.Gold += waves[currentWave].reward;

                    currentEnemy = 0;
                    lastSpawnTime = Time.time;
                }
            }
            else
            {
                //wave is still going continue as normal
            }
        }
    }
}
