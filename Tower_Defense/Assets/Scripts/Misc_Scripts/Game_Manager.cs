//This script is a modified version of the placemonster code found at
//https://www.raywenderlich.com/269-how-to-create-a-tower-defense-game-in-unity-part-1#toc-anchor-018
//Author:Blake Henderson
//Date:8/29/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Game_Manager : MonoBehaviour
{
    public int startingGold = 500;
    /// <summary>
    /// The text mesh pro text that displays the gold of the player
    /// </summary>
    public TMP_Text goldText;
    /// <summary>
    /// The text mesh pro text that displays the current wave
    /// </summary>
    public TMP_Text waveText;
    /// <summary>
    /// The text mesh pro text that displays the current health
    /// </summary>
    public TMP_Text livesText;
    /// <summary>
    /// The panel that holds the lose text
    /// </summary>
    public GameObject losePanel;
    /// <summary>
    /// The gold the player has
    /// </summary>
    private int gold;
    /// <summary>
    /// The public version for the private gold variable
    /// </summary>
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldText.text = "GOLD: " + gold;
        }
    }
    /// <summary>
    /// The wave the player is on
    /// </summary>
    private int wave;
    /// <summary>
    /// The public version for the private wave variable
    /// </summary>
    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
            waveText.text = "WAVE: " + (wave + 1);
        }
    }

    private int lives;
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
            if (lives <= 0 && !gameOver)
            {
                livesText.text = "LIVES: 0";
                gameOver = true;
                losePanel.SetActive(true);
            }
            else
            {
                livesText.text = "LIVES: " + lives;
            }
        }
    }
    /// <summary>
    /// Determines if the game is over
    /// </summary>
    public bool gameOver = false;
    private void Start()
    {
        Gold = startingGold;
        Wave = 0;
        Lives = 10;
    }
    private void Update()
    {
        
    }
}
