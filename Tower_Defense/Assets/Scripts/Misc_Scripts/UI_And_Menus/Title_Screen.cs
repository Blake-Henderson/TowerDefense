using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Screen : MonoBehaviour
{
    public float blinkFrequency = 0.5f;
    public GameObject text;
    private float timer;
    private bool blink;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        text.SetActive(blink);
        if(timer >= blinkFrequency)
        {
            blink = !blink;
            timer = 0;
        }
        if (Input.anyKey)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
        }
    }
}
