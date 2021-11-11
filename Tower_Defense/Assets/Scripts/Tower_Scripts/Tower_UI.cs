//Author:Blake Henderson
//Date:11/10/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower_UI : MonoBehaviour
{
    public Canvas canvas;
    public GameObject position;
    public Button button;
    public Text text;
    public Tower_Data tower;
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }
    private void OnMouseOver()
    {
        if (tower.getNextLevel() != null)
        {
            canvas.gameObject.SetActive(true);
            button.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);
            text.text = "Upgrade \n" + tower.getNextLevel().cost;
            
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }
    private void OnMouseExit()
    {
        canvas.gameObject.SetActive(false);
    }
}
