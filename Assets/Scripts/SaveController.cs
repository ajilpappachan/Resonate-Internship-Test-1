using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    //Save the Current State
    public void SaveRecord(int correctLetters, int incorrectLetters, int hints)
    {
        SaveObject saveObject = new SaveObject();
        saveObject.correctLetters = correctLetters;
        saveObject.incorrectLetters = incorrectLetters;
        saveObject.hints = hints;
        string saveData = JsonUtility.ToJson(saveObject);
        SaveManager.Save(saveData);
    }

    //Load Saved State
    public SaveObject LoadRecord()
    {
        string loadedData = SaveManager.Load();
        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(loadedData);
        return loadedSaveObject;
    }
}
