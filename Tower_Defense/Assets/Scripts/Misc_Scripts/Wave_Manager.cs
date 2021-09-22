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
        public List<GameObject> eneimies;
        public int reward;
    }
    /// <summary>
    /// The waves of enemies
    /// </summary>
    public Wave[] waves;
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
        gameManager = gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].eneimies[currentEnemy].GetComponent<Enemy_Data>().spawnTime;
            if ((timeInterval > spawnInterval) && currentEnemy < waves[currentWave].eneimies.Count)
            {
                //spawn a runner enemy
                if (waves[currentWave].eneimies[currentEnemy].gameObject.tag.Equals("Runner")){
                    lastSpawnTime = Time.time;

                    GameObject newEnemy = (GameObject) Instantiate(waves[currentWave].eneimies[currentEnemy],
                        waves[currentWave].eneimies[currentEnemy].GetComponent<Enemy_Data>().path.waypoints[0].transform);
                    newEnemy.GetComponent<Road_Enemy_AI>().waypoints =
                        waves[currentWave].eneimies[currentEnemy].GetComponent<Enemy_Data>().path.waypoints;
                }
                //spawn a breaker enemey
                else
                {
                    //do nothing for now
                }
            }
        }
        if (currentEnemy == waves[currentWave].eneimies.Count &&
            GameObject.FindGameObjectWithTag("Runner") == null &&
            GameObject.FindGameObjectWithTag("Breaker") == null)
        {
            gameManager.Gold += waves[currentWave].reward;
            gameManager.Wave++;       
            if(currentWave == waves.Length)
            {
                //win
            }
            currentEnemy = 0;
            lastSpawnTime = Time.time;
        }
    }
}
