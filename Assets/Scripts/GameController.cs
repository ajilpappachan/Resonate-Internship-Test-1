using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Variables
    public GameObject answerSlot;
    public Image levelImage;
    private LevelObject levelObject;
    private List<GameObject> LetterBoxes;
    private List<GameObject> AnswerSlots;
    private int currentSlot;
    private List<int> emptySlots;
    private int totalStars;

    //Audio Elements
    private AudioManager audioManager;

    //Save Elements
    private SaveController saveController;
    public int correctLetters;
    public int incorrectLetters;
    public int hints;

    //UI Elements
    public GameObject LetterboxesUI;
    public GameObject AnswerSlotsUI;
    public GameObject MainUI;
    public GameObject PauseUI;
    public GameObject SubmitButton;
    public GameObject CorrectScreen;
    public GameObject WrongScreen;
    public Button hintButton;

    //Initialise Everything
    private void Awake()
    {
        //Initialise Variables
        LetterBoxes = new List<GameObject>();
        AnswerSlots = new List<GameObject>();
        currentSlot = 0;
        totalStars = 0;
        emptySlots = new List<int>();
        GetComponent<LevelObjectManager>().Initialise();
        levelObject = GetComponent<LevelObjectManager>().getRandomObject();

        //Initialise Save Controller
        SaveManager.Initialise();
        saveController = GetComponent<SaveController>();
        saveController.ClearLevelData();
        incorrectLetters = 0;
        correctLetters = 0;
        hints = 0;

        //Initialise Audio
        audioManager = FindObjectOfType<AudioManager>();

        //Start first level
        startLevel(levelObject);
    }

    //Pause, Resume and Reload Gameplay
    public void Pause()
    {
        audioManager.playButton();
        MainUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    public void Resume()
    {
        audioManager.playButton();
        PauseUI.SetActive(false);
        MainUI.SetActive(true);
    }

    public void Reload()
    {
        audioManager.playButton();
        foreach (GameObject box in LetterBoxes)
        {
            box.GetComponent<LetterBox>().Reload();
            emptySlots.Add(currentSlot++);
        }
        currentSlot = 0;
    }

    //Quit to Main Menu
    public void Quit()
    {
        audioManager.playButton();
        SceneManager.LoadScene("Quit");
    }

    //Submit Answer
    public void Submit()
    {
        audioManager.playButton();
        MainUI.SetActive(false);
        string formedWord = "";
        foreach(GameObject slot in AnswerSlots)
        {
            formedWord += slot.GetComponent<AnswerSlot>().getletter();
        }
        if(formedWord == levelObject.objectName)
        {
            showCorrectScreen();
        }
        else
        {
            showWrongScreen();
        }
    }

    //Show Correct and Wrong Screens
    private void showCorrectScreen()
    {
        saveController.updateCompletedLevels(levelObject.name);
        audioManager.playCorrect();
        CorrectScreen.SetActive(true);
        foreach(GameObject slot in AnswerSlots)
        {
            slot.GetComponent<AnswerSlot>().giveStar();
        }
    }

    private void showWrongScreen()
    {
        audioManager.playWrong();
        WrongScreen.SetActive(true);
        GameObject.FindGameObjectWithTag("CorrectWordUI").GetComponent<Text>().text = levelObject.objectName;
    }

    //Load Next Level
    public void Next()
    {
        //Reset Level Stats
        audioManager.playButton();
        LetterBoxes.Clear();
        AnswerSlots.Clear();
        currentSlot = 0;
        emptySlots.Clear();
        MainUI.SetActive(true);

        //Destroy Unnecessary GameObjects
        foreach (GameObject slot in GameObject.FindGameObjectsWithTag("AnswerSlot"))
        {
            Destroy(slot);
        }
        foreach(GameObject letter in GameObject.FindGameObjectsWithTag("LetterBox"))
        {
            letter.GetComponent<LetterBox>().Reload();
            letter.SetActive(false);
        }
        foreach(GameObject star in GameObject.FindGameObjectsWithTag("Star"))
        {
            totalStars++;
            saveController.SaveRecord(correctLetters, incorrectLetters, hints, totalStars);
            GameObject.FindGameObjectWithTag("TotalStars").GetComponent<Text>().text = totalStars.ToString();
            Destroy(star);
        }
        foreach(GameObject starSlot in GameObject.FindGameObjectsWithTag("StarSlot"))
        {
            Destroy(starSlot);
        }

        //Save the game Status and Load next level
        saveController.updatePreviousLevel(levelObject.name);
        if(saveController.getCompletedLevelsLength() == GetComponent<LevelObjectManager>().getLevelObjectsLength())
        {
            SceneManager.LoadScene("Quit");
        }
        else
        {
            while (true)
            {
                levelObject = GetComponent<LevelObjectManager>().getRandomObject();
                SaveObject loadedData = saveController.LoadRecord();
                if (!(loadedData.lastLevel == levelObject.name) && !(loadedData.completedLevels.Contains(levelObject.name)))
                {
                    break;
                }
            }
            CorrectScreen.SetActive(false);
            WrongScreen.SetActive(false);
            SubmitButton.SetActive(false);
            startLevel(levelObject);
        }
    }

    //Level Logic
    public void startLevel(LevelObject levelObject)
    {
        //Load Image
        levelImage.sprite = levelObject.objectImage;

        //Create LetterBoxes
        foreach (char letter in levelObject.objectName)
        {
            GameObject child;
            while(true)
            {
                child = LetterboxesUI.transform.GetChild(Random.Range(0, 21)).gameObject;
                if (!child.activeInHierarchy)
                {
                    child.GetComponent<LetterBox>().Initialise(letter, this);
                    child.SetActive(true);
                    break;
                }
            }
            child.GetComponentInChildren<Text>().text = letter.ToString();
            LetterBoxes.Add(child);

            //Create Answer Slots
            GameObject slot = Instantiate(answerSlot, AnswerSlotsUI.transform);
            slot.GetComponent<AnswerSlot>().Initialise(currentSlot, slot.transform.position, child, this);
            AnswerSlots.Add(slot);
            emptySlots.Add(currentSlot++);
        }
        currentSlot = 0;
        checkHintStatus();
    }

    //Get the current answer slot
    public GameObject getCurrentAnswerSlot()
    {
        emptySlots.Sort();
        GameObject emptySlot = AnswerSlots[emptySlots[0]];
        emptySlots.RemoveAt(0);
        return emptySlot;
    }

    //Enable/Disable Hint Button
    public void setHintActive(bool value)
    {
        hintButton.interactable = value;
    }

    //Check the current hint Status
    public void checkHintStatus()
    {
        StopCoroutine("Hint");
        setHintActive(false);
        StartCoroutine("Hint");
    }

    //Get Hint for the current slot
    public void getHint()
    {
        getCurrentAnswerSlot().GetComponent<AnswerSlot>().useHint();
        checkHintStatus();
    }

    IEnumerator Hint()
    {
        yield return new WaitForSeconds(30.0f);
        setHintActive(true);
        StopCoroutine("Hint");
    }

    //Add or remove any empty slots
    public void addEmptySlot(int index)
    {
        emptySlots.Add(index);
    }

    public void removeSlot(int index)
    {
        emptySlots.Remove(index);
    }

    //Check if all slots are full
    public void checkFullSlots()
    {
        if(emptySlots.Count == 0)
        {
            SubmitButton.SetActive(true);
        }
        else
        {
            SubmitButton.SetActive(false);
        }
    }

    //Save States
    public void saveCorrectLetter()
    {
        saveController.SaveRecord(++correctLetters, incorrectLetters, hints, totalStars);
    }

    public void saveIncorrectLetter()
    {
        saveController.SaveRecord(correctLetters, ++incorrectLetters, hints, totalStars);
    }

    public void saveHint()
    {
        saveController.SaveRecord(correctLetters, incorrectLetters, ++hints, totalStars);
    }
}
