//Author:Blake Henderson
//Date:11/10/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Controller : MonoBehaviour
{
    /// <summary>
    /// The health of the player
    /// </summary>
    public int health = 10;
    /// <summary>
    /// The projectile the player fires
    /// </summary>
    public GameObject projectile;
    /// <summary>
    /// The movement speed of the player
    /// </summary>
    public float speed = 2.0f;
    /// <summary>
    /// The firerate of the player
    /// </summary>
    public float fireRate = 1.0f;
    /// <summary>
    /// How long the player is invincible after being hit by runners
    /// </summary>
    public float iTime = .5f;
    /// <summary>
    /// The panel that holds the lose text
    /// </summary>
    public GameObject losePanel;
    /// <summary>
    /// The text that displays the player's health
    /// </summary>
    public TMP_Text healthText;
    public AudioClip hurtNoise;
    public AudioClip fireNoise;
    /// <summary>
    /// Where the projectiles are fired from
    /// </summary>
    private Transform firePoint;
    /// <summary>
    /// The player animator
    /// </summary>
    private Animator animator;
    /// <summary>
    /// The rigid body of the player
    /// </summary>
    private Rigidbody2D rb2d;
    /// <summary>
    /// The timer for keeping track between shots
    /// </summary>
    private float shotTimer = 20.0f;
    /// <summary>
    /// The timer for how long the player is immune to runner damage
    /// </summary>
    private float damageTimer = 20.0f;
    /// <summary>
    /// The movement the player is doing
    /// </summary>
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        firePoint = transform.Find("Fire Point");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthText.text = ("HEALTH: " + health);
    }

    private void Update()
    {
        shotTimer += Time.deltaTime;
        damageTimer += Time.deltaTime;
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //animation handling borrows from my previous project RNGuy's character controller
        //https://github.com/BrandonSloan/RNGuy
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (mousePos.x <= transform.position.x)
        {
            animator.SetBool("Facing Left", true);
            animator.SetBool("Facing Right", false);
        }
        else
        {
            animator.SetBool("Facing Right", true);
            animator.SetBool("Facing Left", false);
        }

        if (Input.GetMouseButton(0) && shotTimer >= fireRate)
        {
            animator.SetTrigger("Attack");
            animator.SetBool("Moving Right", false);
            animator.SetBool("Moving Left", false);
            shotTimer = 0.0f;
        }
        if (movement.y > 0)
        {
            animator.SetBool("Moving Right", animator.GetBool("Facing Right"));
            animator.SetBool("Moving Left", animator.GetBool("Facing Left"));
        }
        if (movement.y < 0)
        {
            animator.SetBool("Moving Right", animator.GetBool("Facing Right"));
            animator.SetBool("Moving Left", animator.GetBool("Facing Left"));
        }
        if (movement.x > 0)
        {
            animator.SetBool("Moving Right", true);
            animator.SetBool("Moving Left", false);
        }
        if (movement.x < 0)
        {
            animator.SetBool("Moving Right", false);
            animator.SetBool("Moving Left", true);
        }
        if (movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("Moving Right", false);
            animator.SetBool("Moving Left", false);
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        healthText.text = ("HEALTH: " + health);
        if (health <= 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        rb2d.MovePosition(rb2d.position + (movement * speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Runner" && damageTimer >= iTime)
        {
            if (health > 0)
            {
                takeDamage();
                damageTimer = 0.0f;
            }           
        }
        if(collision.tag == "Breaker" && damageTimer >= iTime && collision.gameObject.GetComponent<Breaker_Enemy_AI>().targetsPlayer)
        {
            if (health > 0)
            {
                takeDamage();
                damageTimer = 0.0f;
            }
        }
    }
    public void takeDamage()
    {
        health -= 1;
        SoundManager.instance.playDeathNoise(hurtNoise);
    }

    /// <summary>
    /// Actually fires a projectile very similar to the firing in the player controller from my previous project
    /// https://github.com/Blake-Henderson/Midterm_2D_Game
    /// </summary>
    void fire()
    {
        SoundManager.instance.playFireNoise(fireNoise);
        Vector2 mouseScreen = Input.mousePosition;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouseScreen);
        float angle = Mathf.Atan2(mousePos.y - firePoint.position.y,
            mousePos.x - firePoint.position.x) * Mathf.Rad2Deg;
        SimplePool.Spawn(projectile, firePoint.position, Quaternion.Euler(0, 0, angle));
        //animator.ResetTrigger("Attack");
    }
}
