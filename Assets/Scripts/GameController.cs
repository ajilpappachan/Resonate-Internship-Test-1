using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    class SaveObject
    {
        public int correctLetters;
        public int incorrectLetters;
        public int hints;
    }

    //Initialise Everything
    private void Awake()
    {
        SaveManager.Initialise();
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
