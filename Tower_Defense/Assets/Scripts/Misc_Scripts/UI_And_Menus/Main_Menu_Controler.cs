//Author:Blake Henderson
//Date:11/10/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu_Controler : MonoBehaviour
{
    public void playButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
    }
    public void tutorialButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }
    public void levelButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_Select");
    }
    public void quitButton()
    {
        Application.Quit();
    }
}
