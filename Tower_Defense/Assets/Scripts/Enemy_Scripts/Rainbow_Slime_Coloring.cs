//Author:Blake Henderson
//Date:8/29/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow_Slime_Coloring : MonoBehaviour
{
    /// <summary>
    /// The sprite of the rainbow slime to change the color of
    /// </summary>
    public SpriteRenderer sprite;
    public float minimumColor = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        changeColor();
    }
    public void changeColor()
    {
        //generates a new random color for the sprite
        sprite.color = new Color(Random.Range(minimumColor, 1f), Random.Range(minimumColor, 1f), Random.Range(minimumColor, 1f), 1);
    }
}
