using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu_Controler : MonoBehaviour
{
    public void playButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
    }
    public void quitButton()
    {
        Application.Quit();
    }
}
