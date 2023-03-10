//This code was written with help of this tutorial
//https://www.youtube.com/watch?v=jvtFUfJ6CP8
//Author:Blake Henderson
//Date:2/23/23
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Pathfinding;

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
    /// Distance between AI and waypoint before choosing a new one
    /// </summary>
    public float nextWayPointDistance = .5f;
    /// <summary>
    /// The current index on the path
    /// </summary>
    int currentWaypointIndex = 0;
    /// <summary>
    /// Has the AI reached the end of the current path
    /// </summary>
    bool reachedEndOfPath = false;
    /// <summary>
    /// How much damage the enemy does to a tower
    /// </summary>
    public int towerDamage = 10;
    /// <summary>
    /// How often the enemy attacks
    /// </summary>
    public float attackRate = 1.0f;
    /// <summary>
    /// The range of the attack
    /// </summary>
    public float attackRange = 1.5f;
    /// <summary>
    /// How often the enemy attempts to recalibrate its target
    /// </summary>
    public float retargetRate = 5.0f;
    /// <summary>
    /// The health of the enemy
    /// </summary>
    public Health_Bar health;
    /// <summary>
    /// The graphics of the enemy
    /// </summary>
    public Transform enemyGFX;
    /// <summary>
    /// List of all potential targets
    /// </summary>
    [HideInInspector]
    public List<GameObject> potentialTargets;
    /// <summary>
    /// The target the breaker enemy is currently going after
    /// </summary>
    public GameObject target = null;
    /// <summary>
    /// keeps track of the time of attacks
    /// </summary>
    private float attackTimer = 0.0f;
    /// <summary>
    /// keeps track of time between retargeting
    /// </summary>
    private float retargetTimer = 0.0f;
    /// <summary>
    /// The speed of the enemy
    /// </summary>
    private float speed = 1.0f;
    /// <summary>
    /// The seeker attached to the AI
    /// </summary>
    Seeker seeker;
    /// <summary>
    /// The rigidbody on the AI
    /// </summary>
    Rigidbody2D rb;
    /// <summary>
    /// The path the AI is on
    /// </summary>
    Path path;
    // Start is called before the first frame update
    public void init()
    {
        attackTimer = 0.0f;
        retargetTimer = 0.0f;
        speed = gameObject.GetComponent<Enemy_Data>().speed;
        health.currentHealth = health.maxHealth;
        currentWaypointIndex = 0;
        reachedEndOfPath = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        retarget();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    /// <summary>
    /// Resets the path upon completion
    /// </summary>
    /// <param name="p"></param>
    void onPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypointIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || (targetsTowers && targetsPlayer))
        {
            attackTimer += Time.deltaTime;
            retargetTimer += Time.deltaTime;
            if (retargetTimer >= retargetRate)
            {
                retarget();
                retargetTimer = 0f;
            }
        }
        attackTimer += Time.deltaTime;
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange && attackTimer >= attackRate)
        {
            if (target.tag == "Tower")
            {
                target.transform.Find("Tower").GetComponent<Tower_Data>().health.currentHealth -= towerDamage;
            }
            else
            {
                target.GetComponent<Player_Controller>().health -= 1;
            }
            attackTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypointIndex >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypointIndex] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypointIndex]);

        if (distance < nextWayPointDistance)
        {
            currentWaypointIndex++;
        }

        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    /// <summary>
    /// Updates the path
    /// </summary>
    public void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.transform.position, onPathComplete);
        }
    }
    /// <summary>
    /// Finds a new target for the 
    /// </summary>
    public void retarget()
    {
        potentialTargets.Clear();
        //grabs all towers on map
        if (targetsTowers)
        {
            foreach (GameObject Tower in GameObject.FindGameObjectsWithTag("Tower"))
            {
                potentialTargets.Add(Tower);
            }
        }
        if (targetsPlayer)
        {
            potentialTargets.Add(GameObject.FindGameObjectWithTag("Player"));
        }
        if (potentialTargets.Count == 0)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            UpdatePath();
            return;
        }
        GameObject closestTarget = potentialTargets[0];
        float shortestDistance = float.MaxValue;
        foreach (GameObject temp in potentialTargets)
        {
            float distance = Vector3.Distance(gameObject.transform.position, temp.transform.position);
            if (distance <= shortestDistance)
            {
                closestTarget = temp;
                shortestDistance = distance;
            }
        }
        target = closestTarget;
        UpdatePath();
    }
}
