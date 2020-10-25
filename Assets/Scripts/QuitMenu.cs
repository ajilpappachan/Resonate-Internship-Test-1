using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    public Text correctLetters;
    public Text incorrectLetters;
    public Text hints;
    public Text totalStars;
    private SaveController saveController;
    public AudioClip quitSfx;

    //Show the data from the save file
    private void Awake()
    {
        saveController = GetComponent<SaveController>();
        SaveObject loadedData = saveController.LoadRecord();
        correctLetters.text = loadedData.correctLetters.ToString();
        incorrectLetters.text = loadedData.incorrectLetters.ToString();
        totalStars.text = loadedData.totalStars.ToString();
        hints.text = loadedData.hints.ToString();
        FindObjectOfType<AudioManager>().playQuit();
    }

    //Go back to main menu
    public void Quit()
    {
        FindObjectOfType<AudioManager>().playButton();
        SceneManager.LoadScene("MainMenu");
    }
}
