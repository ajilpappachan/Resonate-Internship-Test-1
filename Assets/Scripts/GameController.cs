using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private GameObject Image;
    public GameObject LetterboxesUI;
    public GameObject AnswerSlotsUI;
    public GameObject answerSlot;
    public Image levelImage;
    private LevelObject levelObject;
    private List<GameObject> AnswerSlots;
    private int currentSlot;


    class SaveObject
    {
        public int correctLetters;
        public int incorrectLetters;
        public int hints;
    }

    //Initialise Everything
    private void Awake()
    {
        AnswerSlots = new List<GameObject>();
        currentSlot = 0;
        SaveManager.Initialise();
        levelObject = GetComponent<LevelObjectManager>().getRandomObject();
        startLevel(levelObject);
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
            child.GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();
            AnswerSlots.Add(Instantiate(answerSlot, AnswerSlotsUI.transform));
        }
    }

    //Get the current answer slot
    public GameObject getCurrentAnswerSlot()
    {
        return AnswerSlots[currentSlot++];
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
