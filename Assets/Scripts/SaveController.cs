using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    SaveObject saveObject = new SaveObject();

    //Save the Current State
    private void Save()
    {
        string saveData = JsonUtility.ToJson(saveObject);
        SaveManager.Save(saveData);
    }
    public void SaveRecord(int correctLetters, int incorrectLetters, int hints, int totalStars)
    {
        saveObject.correctLetters = correctLetters;
        saveObject.incorrectLetters = incorrectLetters;
        saveObject.hints = hints;
        saveObject.totalStars = totalStars;
        Save();
    }

    public void updatePreviousLevel(string previousLevel)
    {
        saveObject.lastLevel = previousLevel;
        Save();
    }

    public void updateCompletedLevels(string finishedLevel)
    {
        saveObject.completedLevels.Add(finishedLevel);
        Save();
    }

    //Get the number of levels completed
    public int getCompletedLevelsLength()
    {
        return saveObject.completedLevels.Count;
    }

    //Clear LevelData for New Game
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
