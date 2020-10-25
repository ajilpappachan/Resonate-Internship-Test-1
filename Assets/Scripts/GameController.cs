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

    //Initialise Everything
    private void Awake()
    {
        LetterBoxes = new List<GameObject>();
        AnswerSlots = new List<GameObject>();
        currentSlot = 0;
        totalStars = 0;
        emptySlots = new List<int>();
        levelObject = GetComponent<LevelObjectManager>().getRandomObject();

        SaveManager.Initialise();
        saveController = GetComponent<SaveController>();
        incorrectLetters = 0;
        correctLetters = 0;
        hints = 0;

        audioManager = FindObjectOfType<AudioManager>();

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
        audioManager.playButton();
        LetterBoxes.Clear();
        AnswerSlots.Clear();
        currentSlot = 0;
        emptySlots.Clear();
        MainUI.SetActive(true);
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
        levelObject = GetComponent<LevelObjectManager>().getRandomObject();
        CorrectScreen.SetActive(false);
        WrongScreen.SetActive(false);
        SubmitButton.SetActive(false);
        startLevel(levelObject);
    }

    //Level Logic
    public void startLevel(LevelObject levelObject)
    {
        levelImage.sprite = levelObject.objectImage;
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
            GameObject slot = Instantiate(answerSlot, AnswerSlotsUI.transform);
            slot.GetComponent<AnswerSlot>().Initialise(currentSlot, slot.transform.position, child, this);
            AnswerSlots.Add(slot);
            emptySlots.Add(currentSlot++);
        }
        currentSlot = 0;
    }

    //Get the current answer slot
    public GameObject getCurrentAnswerSlot()
    {
        emptySlots.Sort();
        GameObject emptySlot = AnswerSlots[emptySlots[0]];
        emptySlots.RemoveAt(0);
        return emptySlot;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
