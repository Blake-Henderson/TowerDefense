//This code was written with help of this tutorial
//https://www.youtube.com/watch?v=jvtFUfJ6CP8
//Author:Blake Henderson
//Date:12/5/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Breaker_Enemy_AI : MonoBehaviour
{
    /// <summary>
    /// Tells the AI to target the player
    /// </summary>
    public bool targetsPlayer = true;
    /// <summary>
    /// Tells the AI to target towers
    /// </summary>
    public bool targetsTowers = false;
    /// <summary>
    /// How much damage the enemy does to a tower
    /// </summary>
    public int towerDamage = 10;
    /// <summary>
    /// How often the enemy attacks
    /// </summary>
    public float attackRate = 1.0f;
    /// <summary>
    /// How often the enemy attempts to recalibrate its target
    /// </summary>
    public float retargetRate = 5.0f;
    /// <summary>
    /// List of all potential targets
    /// </summary>
    [HideInInspector]
    public List<GameObject> potentialTargets;
    /// <summary>
    /// The target the breaker enemy is currently going after
    /// </summary>
    private GameObject target = null;
    /// <summary>
    /// keeps track of the time of attacks
    /// </summary>
    private float attackTimer = 0.0f;
    /// <summary>
    /// keeps track of time between retargeting
    /// </summary>
    private float retargetTimer = 0.0f;
    // Start is called before the first frame update
    public void init()
    {
        attackTimer = 0.0f;
        retargetTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        retargetTimer += Time.deltaTime;
        if(target == null || (retargetTimer >= retargetRate && targetsPlayer))
        {
            retarget();
        }
    }

    public void retarget()
    {
        //grabs all towers on map
        if (targetsTowers)
        {
            potentialTargets = GameObject.FindGameObjectsWithTag("Tower").ToList();
        }
        if (targetsPlayer)
        {
            potentialTargets.Add(GameObject.FindGameObjectWithTag("Player"));
        }
        GameObject closestTarget = potentialTargets[0];
        float shortestDistance = 99999.0f;
        foreach(GameObject temp in potentialTargets)
        {
            float distance = Vector3.Distance(gameObject.transform.position, temp.transform.position);
            if (distance <= shortestDistance)
            {
                closestTarget = temp;
                shortestDistance = distance;
            }
        }
        target = closestTarget;
    }
}
