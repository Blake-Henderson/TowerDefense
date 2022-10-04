using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Select : MonoBehaviour
{
    public void mainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void levelOne()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void levelTwo()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void levelThree()
    {
        SceneManager.LoadScene("Level_3");
    }
    public void levelFour()
    {
        SceneManager.LoadScene("Level_4");
    }
    public void levelFive()
    {
        SceneManager.LoadScene("Level_5");
    }
}
