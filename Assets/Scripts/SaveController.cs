using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    SaveObject saveObject = new SaveObject();

    //Save the Current State
    public void SaveRecord(int correctLetters, int incorrectLetters, int hints, int totalStars)
    {
        saveObject.correctLetters = correctLetters;
        saveObject.incorrectLetters = incorrectLetters;
        saveObject.hints = hints;
        saveObject.totalStars = totalStars;
        string saveData = JsonUtility.ToJson(saveObject);
        SaveManager.Save(saveData);
    }

    public void updatePreviousLevel(string previousLevel)
    {
        saveObject.lastLevel = previousLevel;
        string saveData = JsonUtility.ToJson(saveObject);
        SaveManager.Save(saveData);
    }

    public void updateCompletedLevels(string finishedLevel)
    {
        saveObject.completedLevels.Add(finishedLevel);
        string saveData = JsonUtility.ToJson(saveObject);
        SaveManager.Save(saveData);
    }

    public int getCompletedLevelsLength()
    {
        return saveObject.completedLevels.Count;
    }

    public void ClearLevelData()
    {
        saveObject.lastLevel = "";
        saveObject.completedLevels.Clear();
    }

    //Load Saved State
    public SaveObject LoadRecord()
    {
        string loadedData = SaveManager.Load();
        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(loadedData);
        return loadedSaveObject;
    }
}
