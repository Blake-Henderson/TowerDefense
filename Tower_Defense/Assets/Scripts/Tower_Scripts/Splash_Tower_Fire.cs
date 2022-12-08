//This script is a modified version of the placemonster code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:11/3/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash_Tower_Fire : MonoBehaviour
{
    /// <summary>
    /// The enimies in range
    /// </summary>
    public List<GameObject> enemiesInRange;
    public AudioClip fireClip;
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
    private ParticleSystem pfx;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerData = gameObject.GetComponentInChildren<Tower_Data>();
        firePoint = gameObject.transform.Find("Fire Point").transform;
        GameObject gm = GameObject.Find("Game Manager");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Time.time - lastShotTime > towerData.CurrentLevel.fireRate && enemiesInRange.Count != 0)
        {
            //Debug.Log("Firing");
            pfx = Instantiate(towerData.CurrentLevel.bullet.GetComponent<ParticleSystem>(),
            firePoint.transform.position, new Quaternion(-90, 0, 0, 0));
            Destroy(pfx, 2.0f);
            Invoke("Fire",.75f);
            lastShotTime = Time.time;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("something entered");
        if (other.gameObject.tag.Equals("Runner") || other.gameObject.tag.Equals("Breaker"))
        {
            //Debug.Log("enemy added");
            enemiesInRange.Add(other.gameObject);
            Enemy_Destruction_Delegate del =
                other.gameObject.GetComponent<Enemy_Destruction_Delegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Runner") || other.gameObject.tag.Equals("Breaker"))
        {
            //Debug.Log("enemy removed");
            enemiesInRange.Remove(other.gameObject);
            Enemy_Destruction_Delegate del =
                other.gameObject.GetComponent<Enemy_Destruction_Delegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    private void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void Fire()
    {
        if (fireClip != null)
            SoundManager.instance.playFireNoise(fireClip);
        foreach (GameObject enemy in enemiesInRange)
        {
            Transform healthBarTransform = enemy.transform.Find("HealthBar");
            Health_Bar healthBar =
                healthBarTransform.gameObject.GetComponent<Health_Bar>();
            healthBar.currentHealth -= towerData.CurrentLevel.damage;
        }
    }
}
