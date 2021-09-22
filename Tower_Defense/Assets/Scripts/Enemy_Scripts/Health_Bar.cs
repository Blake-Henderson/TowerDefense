//This script is a modified version of the placemonster code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:8/29/21using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Bar : MonoBehaviour
{
    private float maxHealth = 100;
    public float currentHealth = 100;
    private float originalScale;
    private void Start()
    {
        originalScale = gameObject.transform.localScale.x;
        maxHealth = gameObject.transform.parent.gameObject.GetComponent<Enemy_Data>().health;
        currentHealth = maxHealth;
    }
    private void Update()
    {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = currentHealth / maxHealth * originalScale;
        gameObject.transform.localScale = tmpScale;
    }
}
