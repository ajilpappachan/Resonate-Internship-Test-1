using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //UI Elements
    public GameObject LetterboxesUI;
    public GameObject AnswerSlotsUI;
    public GameObject MainUI;
    public GameObject PauseUI;
    public GameObject SubmitButton;


    class SaveObject
    {
        public int correctLetters;
        public int incorrectLetters;
        public int hints;
    }

    //Initialise Everything
    private void Awake()
    {
        LetterBoxes = new List<GameObject>();
        AnswerSlots = new List<GameObject>();
        currentSlot = 0;
        emptySlots = new List<int>();
        SaveManager.Initialise();
        levelObject = GetComponent<LevelObjectManager>().getRandomObject();
        startLevel(levelObject);
    }

    //Pause, Resume and Reload Gameplay
    public void Pause()
    {
        MainUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        MainUI.SetActive(true);
    }

    public void Reload()
    {
        foreach(GameObject box in LetterBoxes)
        {
            box.GetComponent<LetterBox>().Reload();
        }
        currentSlot = 0;
        emptySlots.Clear();
    }

    //Quit to Main Menu
    public void Quit()
    {

    }

    //Submit Answer
    public void Submit()
    {

    }

    //Save the Current State
    private void SaveRecord(string message, int number)
    {
        SaveObject saveObject = new SaveObject();
        string saveData = JsonUtility.ToJson(saveObject);
        SaveManager.Save(saveData);
    }

    //Load Saved State
    private SaveObject LoadRecord()
    {
        string loadedData = SaveManager.Load();
        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(loadedData);
        return loadedSaveObject;
    }

    //Level Logic
    public void startLevel(LevelObject levelObject)
    {
        levelImage.sprite = levelObject.objectImage;
        foreach (char letter in levelObject.objectName)
        {
            GameObject child = LetterboxesUI.transform.GetChild(Random.Range(0, 21)).gameObject;
            do
            {
                if (!child.activeInHierarchy)
                {
                    child.GetComponent<LetterBox>().Initialise(letter, this);
                    child.SetActive(true);
                }
                else
                {
                    child = LetterboxesUI.transform.GetChild(Random.Range(0, 21)).gameObject;
                }
            }
            while (!child.activeInHierarchy);
            child.GetComponentInChildren<Text>().text = letter.ToString();
            LetterBoxes.Add(child);
            GameObject slot = Instantiate(answerSlot, AnswerSlotsUI.transform);
            slot.GetComponent<AnswerSlot>().Initialise(currentSlot++, slot.transform.position);
            AnswerSlots.Add(slot);
        }
        currentSlot = 0;
    }

    //Get the current answer slot
    public GameObject getCurrentAnswerSlot()
    {
        if(emptySlots.Count == 0)
        {
            return AnswerSlots[currentSlot++];
        }
        else
        {
            emptySlots.Sort();
            GameObject emptySlot = AnswerSlots[emptySlots[0]];
            emptySlots.RemoveAt(0);
            Debug.Log(emptySlots);
            return emptySlot;
        }
    }

    //Add any empty slots
    public void addEmptySlot(int index)
    {
        emptySlots.Add(index);
    }

    //Check if all slots are full
    public void checkFullSlots()
    {
        if(currentSlot == AnswerSlots.Count && emptySlots.Count == 0)
        {
            SubmitButton.SetActive(true);
        }
        else
        {
            SubmitButton.SetActive(false);
        }
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
