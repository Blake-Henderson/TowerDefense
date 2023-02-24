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
    /// The enemy the wave is currently on
    /// </summary>
    private int currentEnemy = 0;
    /// <summary>
    /// The time between enemies
    /// </summary>
    private float timer;
    /// <summary>
    /// How long until the enemy needs to spawn
    /// </summary>
    private float spawnDelay;
    /// <summary>
    /// The spawn data for the next enemy
    /// </summary>
    Spawn_Data NextSpawnData;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameOver)
        {
            int currentWave = gameManager.Wave;
            if (currentWave < waves.Length)
            {
                timer += Time.deltaTime;
                if (currentEnemy < waves[currentWave].enemies.Count)
                {
                    NextSpawnData = waves[currentWave].enemies[currentEnemy];
                    spawnDelay = NextSpawnData.spawnTime;

                    if (timer >= spawnDelay)
                    {                        
                        GameObject newEnemy;
                        //spawn a runner enemy
                        if (NextSpawnData.enemy.gameObject.tag.Equals("Runner"))
                        {
                            newEnemy = SimplePool.Spawn(NextSpawnData.enemy, 
                                NextSpawnData.path.waypoints[0].transform.position, Quaternion.identity);
                            newEnemy.GetComponent<Road_Enemy_AI>().init();
                            newEnemy.GetComponent<Road_Enemy_AI>().waypoints =
                                NextSpawnData.path.waypoints;
                        }
                        //spawn a breaker enemey
                        else
                        {
                            //spawns the enemy at a random waypoint on the path
                            newEnemy = SimplePool.Spawn(NextSpawnData.enemy,
                                NextSpawnData.path.waypoints[Random.Range(0,
                                NextSpawnData.path.waypoints.Length)].transform.position,
                                Quaternion.identity);
                            newEnemy.GetComponent<Breaker_Enemy_AI>().init();
                        }
                        timer = 0;
                        currentEnemy++;
                    }
                }
            }
            if (currentWave == waves.Length)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else if (currentEnemy == waves[currentWave].enemies.Count &&
                GameObject.FindGameObjectWithTag("Runner") == null &&
                GameObject.FindGameObjectWithTag("Breaker") == null)
            {
                if (currentWave < waves.Length)
                {
                    gameManager.Gold += waves[currentWave].reward;
                    gameManager.Wave++;
                    currentEnemy = 0;
                    timer = 0;
                }
                else
                {
                    winPanel.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }
            else
            {
                //wave is still going continue as normal
            }
        }
    }
}
