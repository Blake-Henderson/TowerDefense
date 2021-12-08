//Author: Blake Henderson
//Date : 4/20/2021
//modified from projectile script in my previous project
//https://github.com/Blake-Henderson/Midterm_2D_Game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Projectile : MonoBehaviour
{
    /// <summary>
    /// The force behind the bullet
    /// </summary>
    public int thrust = 100;
    /// <summary>
    /// The time it takes the bullet to despwan
    /// </summary>
    public float despawnTime = 1.0f;
    /// <summary>
    /// Damage the projectile does
    /// </summary>
    public int damage = 10;
    /// <summary>
    /// The rigid body of the projectile
    /// </summary>
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("BulletDespawn", despawnTime);
    }
    void Init()
    {
        Invoke("BulletDespawn", despawnTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //for some reason old code wasn't working so I used the line from brakeys
        //https://www.youtube.com/watch?v=wkKsl1Mfp5M&t=783s
        rb2d.velocity = transform.right * thrust;
    }
    /// <summary>
    /// Despawns the bullet after a given number of time.
    /// </summary>
    private void BulletDespawn()
    {
        SimplePool.Despawn(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Runner" || collision.tag == "Breaker")
        {
            Transform healthBarTransform = collision.transform.Find("HealthBar");
            Health_Bar healthBar =
                healthBarTransform.gameObject.GetComponent<Health_Bar>();
            healthBar.currentHealth -= damage;
            SimplePool.Despawn(gameObject);
        }
    }
}
