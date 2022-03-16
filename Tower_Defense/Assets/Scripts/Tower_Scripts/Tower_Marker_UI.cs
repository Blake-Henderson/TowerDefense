//Author:Blake Henderson
//Date:10/13/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower_Marker_UI : MonoBehaviour
{
    public Canvas canvas;
    public List<GameObject> positions;
    public List<Button> buttons;
    public List<GameObject> towers;
    // Start is called before the first frame update
    void Start()
    {        
        canvas.gameObject.SetActive(false);
        for (int i = 0; i < buttons.Count; i++)
        {
            //makes each button display the tower's name and text
            buttons[i].gameObject.GetComponentInChildren<Text>().text = 
                towers[i].GetComponentInChildren<Tower_Data>().towerName + '\n' + 
                towers[i].GetComponentInChildren<Tower_Data>().levels[0].cost;
        }
    }
    private void OnMouseOver()
    {
        if (Pause_Menu.isPaused)
        {
            canvas.gameObject.SetActive(false);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            canvas.gameObject.SetActive(true);
            for (int i = 0; i < positions.Count; i++)
            {
                buttons[i].transform.position = Camera.main.WorldToScreenPoint(positions[i].transform.position);
            }
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
