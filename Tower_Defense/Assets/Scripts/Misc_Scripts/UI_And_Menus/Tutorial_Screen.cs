//Author:Blake Henderson
//Date:11/28/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Screen : MonoBehaviour
{
    public GameObject basicsPanel;
    public GameObject towersPanel;
    public GameObject enemiesPanel;
    // Start is called before the first frame update
    void Start()
    {
        basicsPanel.SetActive(true);
        towersPanel.SetActive(false);
        enemiesPanel.SetActive(false);
    }
    //each of these functions correlates to one of the buttons on the tutorial menu
    public void back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
    }

    public void basics()
    {
        basicsPanel.SetActive(true);
        towersPanel.SetActive(false);
        enemiesPanel.SetActive(false);
    }

    public void towers()
    {
        basicsPanel.SetActive(false);
        towersPanel.SetActive(true);
        enemiesPanel.SetActive(false);
    }

    public void enemies()
    {
        basicsPanel.SetActive(false);
        towersPanel.SetActive(false);
        enemiesPanel.SetActive(true);
    }
}
