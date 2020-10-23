using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectManager : MonoBehaviour
{
    public LevelObject[] levelObjects;
    // Start is called before the first frame update
    void Start()
    {
        //Change the Name to Uppercase Characters
        foreach(LevelObject levelObject in levelObjects)
        {
            levelObject.objectName.ToUpper();
        }
    }

    public LevelObject getRandomObject()
    {
        return (levelObjects[Random.Range(0, levelObjects.Length)]);
    }
}
