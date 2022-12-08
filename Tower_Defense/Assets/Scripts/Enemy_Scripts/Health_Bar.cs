//This script is a modified version of the code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:8/29/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Bar : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;

    public AudioClip deathNoise;
    private float originalScale;
    private Game_Manager game_Manager;
    private void Start()
    {
        originalScale = gameObject.transform.localScale.x;
        if (gameObject.transform.parent.gameObject.tag == "Runner" || gameObject.transform.parent.gameObject.tag == "Breaker")
        {
            maxHealth = gameObject.transform.parent.gameObject.GetComponent<Enemy_Data>().health;
        }
        currentHealth = maxHealth;
        game_Manager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
    }
    private void Update()
    {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = currentHealth / maxHealth * originalScale;
        gameObject.transform.localScale = tmpScale;
        if (gameObject.transform.parent.gameObject.tag == "Runner" || gameObject.transform.parent.gameObject.tag == "Breaker")
        {
            if (currentHealth <= 0)
            {
                if (deathNoise != null)
                    SoundManager.instance.playDeathNoise(deathNoise);

                SimplePool.Despawn(gameObject.transform.parent.gameObject);

                game_Manager.Gold += gameObject.transform.parent.gameObject.GetComponent<Enemy_Data>().reward;
            }
        }
    }
}
