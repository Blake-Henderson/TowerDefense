//This script is a modified version of the code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:9/15/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Destruction_Delegate : MonoBehaviour
{
    public delegate void EnemyDelegate(GameObject enemy);
    public EnemyDelegate enemyDelegate;
    void OnDestroy()
    {
        if (enemyDelegate != null)
        {
            enemyDelegate(gameObject);
        }
    }
}
