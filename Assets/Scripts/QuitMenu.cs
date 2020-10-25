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
    private SaveController saveController;

    private void Awake()
    {
        saveController = GetComponent<SaveController>();
        SaveObject loadedData = saveController.LoadRecord();
        correctLetters.text = loadedData.correctLetters.ToString();
        incorrectLetters.text = loadedData.incorrectLetters.ToString();
        hints.text = loadedData.hints.ToString();
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
