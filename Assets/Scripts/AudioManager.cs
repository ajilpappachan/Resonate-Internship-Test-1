using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Audio Elements
    private AudioSource bgManager;
    public AudioClip bgMusic;
    public AudioClip buttonSfx;
    public AudioClip correctSfx;
    public AudioClip wrongSfx;
    public AudioClip quitSfx;
    public AudioClip starSfx;

    private void Awake()
    {
        //Don't create Audio Manager if already exists
        if(FindObjectOfType<AudioManager>() != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            bgManager = GetComponent<AudioSource>();
            bgManager.clip = bgMusic;
            bgManager.loop = true;
            bgManager.Play();
        }
    }

    //Play Sound Effects
    private void playSound(AudioClip audio)
    {
        AudioSource sfxManager = gameObject.AddComponent<AudioSource>();
        sfxManager.priority = 0;
        sfxManager.clip = audio;
        sfxManager.Play();
    }

    public void playButton()
    {
        playSound(buttonSfx);
    }

    public void playCorrect()
    {
        playSound(correctSfx);
    }

    public void playWrong()
    {
        playSound(wrongSfx);
    }

    public void playStar()
    {
        playSound(starSfx);
    }

    public void playQuit()
    {
        playSound(quitSfx);
    }
}
