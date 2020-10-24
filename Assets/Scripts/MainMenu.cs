﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject help;
    //Functions to Play, Pause, Resume, get Help, and Quit
    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Help()
    {
        help.SetActive(!help.activeInHierarchy);
    }

    public void Quit()
    {
        Application.Quit();
    }
}