using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource effects, hurt;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playDeathNoise(AudioClip clip)
    {
        hurt.PlayOneShot(clip);
    }
    public void playFireNoise(AudioClip clip)
    {
        effects.PlayOneShot(clip);
    }
}
