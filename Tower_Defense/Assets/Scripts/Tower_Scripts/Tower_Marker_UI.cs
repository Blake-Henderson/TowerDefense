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
    // Start is called before the first frame update
    void Start()
    {        
        canvas.gameObject.SetActive(false);
    }
    private void OnMouseOver()
    {
        if (Pause_Menu.isPaused)
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            canvas.gameObject.SetActive(true);
            for (int i = 0; i < positions.Count; i++)
            {
                buttons[i].transform.position = Camera.main.WorldToScreenPoint(positions[i].transform.position);
            }
        }
            
    }
    private void OnMouseExit()
    {
        canvas.gameObject.SetActive(false);
    }
}
