using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject help;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    //Functions to Play, Pause, Resume, get Help, and Quit
    public void Play()
    {
        audioManager.playButton();
        SceneManager.LoadScene("Gameplay");
    }

    public void Help()
    {
        audioManager.playButton();
        help.SetActive(!help.activeInHierarchy);
    }

    public void Quit()
    {
        audioManager.playButton();
        Application.Quit();
    }
}
