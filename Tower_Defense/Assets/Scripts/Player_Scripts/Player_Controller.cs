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
    /// The text that displays the player's health
    /// </summary>
    public TMP_Text healthText;
    /// <summary>
    /// The player animator
    /// </summary>
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
