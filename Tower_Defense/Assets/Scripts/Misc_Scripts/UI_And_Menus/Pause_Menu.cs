using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause_Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
        if(Time.timeScale == 0.0f)
        {
            isPaused = true;
        }
    }

    public void resume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
