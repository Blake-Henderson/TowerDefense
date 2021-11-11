//This script is a modified version of the code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:11/10/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public Vector3 startPosition;

    public float despawnTime = 1f;
    //public Transform targetPosition;

    private float distance;
    private float startTime;

    private Game_Manager game_Manager;

    private void Start()
    {
        Destroy(gameObject, despawnTime);
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, target.transform.position);
        GameObject gm = GameObject.Find("Game Manager");
        game_Manager = gm.GetComponent<Game_Manager>();
        Vector3 direction = startPosition - target.transform.position;
        gameObject.transform.rotation = Quaternion.AngleAxis(
            Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
            new Vector3(0, 0, 1));
    }
    private void Update()
    {
        float timeInterval = Time.time - startTime;

        if (target != null)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, target.transform.position, timeInterval * speed / distance);

            if (gameObject.transform.position.Equals(target.transform.position))
            {
                if (target != null)
                {
                    Transform healthBarTransform = target.transform.Find("HealthBar");
                    Health_Bar healthBar =
                        healthBarTransform.gameObject.GetComponent<Health_Bar>();
                    healthBar.currentHealth -= damage;
                    //if (healthBar.currentHealth <= 0)
                    //{
                    //    Destroy(target);

                    //    game_Manager.Gold += target.GetComponent<Enemy_Data>().reward;
                    //}
                }
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
