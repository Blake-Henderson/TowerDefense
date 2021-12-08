//This script is a modified version of the code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:12/5/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_At_Enemies : MonoBehaviour
{
    /// <summary>
    /// The enimies in range
    /// </summary>
    public List<GameObject> enemiesInRange;
    /// <summary>
    /// When the last shot was taken
    /// </summary>
    private float lastShotTime;
    /// <summary>
    /// The tower data
    /// </summary>
    private Tower_Data towerData;
    /// <summary>
    /// Where the projectile is fired from
    /// </summary>
    private Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerData = gameObject.GetComponentInChildren<Tower_Data>();
        firePoint = gameObject.transform.Find("Fire Point").transform;
    }

    // Update is called once per frame
    void Update()
    {

        GameObject target = null;
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
                float distanceToGoal = enemy.GetComponent<Road_Enemy_AI>().DistanceToGoal();
                if (distanceToGoal < minimalEnemyDistance)
                {
                    target = enemy;
                    minimalEnemyDistance = distanceToGoal;
                }            
        }
        if (target != null)
        {
            if (Time.time - lastShotTime > towerData.CurrentLevel.fireRate)
            {
                Fire(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
        }
    }

    private void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Runner"))
        {
            enemiesInRange.Add(other.gameObject);
            Enemy_Destruction_Delegate del =
                other.gameObject.GetComponent<Enemy_Destruction_Delegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Runner"))
        {
            enemiesInRange.Remove(other.gameObject);
            Enemy_Destruction_Delegate del =
                other.gameObject.GetComponent<Enemy_Destruction_Delegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }
    void Fire(Collider2D target)
    {
        GameObject projPrefab = towerData.CurrentLevel.bullet;


        GameObject newProj = (GameObject)SimplePool.Spawn(projPrefab,firePoint.position,Quaternion.identity);
        Projectile bulletComp = newProj.GetComponent<Projectile>();
        bulletComp.damage = towerData.CurrentLevel.damage;
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = firePoint.position;
        bulletComp.init();        
    }
}
