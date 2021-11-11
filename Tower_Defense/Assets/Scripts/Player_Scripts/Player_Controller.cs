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
    /// How long the player is invincible after being hit
    /// </summary>
    public float iTime = .5f;
    /// <summary>
    /// The text that displays the player's health
    /// </summary>
    public TMP_Text healthText;
    /// <summary>
    /// The player animator
    /// </summary>
    private Animator animator;
    private Rigidbody2D rb2d;
    private float shotTimer = 0.0f;
    private float damageTimer = 0.0f;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
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

        if (Input.GetMouseButtonDown(0) && shotTimer >= fireRate)
        {
            animator.SetTrigger("Attack");
            animator.SetBool("Moving Right", false);
            animator.SetBool("Moving Left", false);
            shotTimer = 0.0f;
        }
        else
        {
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
            if (movement.x == 0 && movement.y == 0)
            {
                animator.SetBool("Moving Right", false);
                animator.SetBool("Moving Left", false);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        animator.ResetTrigger("Attack");
        rb2d.MovePosition(rb2d.position + (movement * speed * Time.deltaTime));
    }
}
