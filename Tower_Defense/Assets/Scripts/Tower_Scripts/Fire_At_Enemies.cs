//This script is a modified version of the placemonster code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:9/15/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_At_Enemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private Tower_Data towerData;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerData = gameObject.GetComponentInChildren<Tower_Data>();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject target = null;
        // 1
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
        // 2
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
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;

        GameObject newProj = (GameObject)Instantiate(projPrefab);
        newProj.transform.position = startPosition;
        Projectile bulletComp = newProj.GetComponent<Projectile>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;
    }
}
