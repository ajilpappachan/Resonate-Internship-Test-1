using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Level Preset", menuName = "Scriptables/Level Preset", order = 1)]
public class LevelObject : ScriptableObject
{
    public Sprite objectImage;
    public string objectName;
}
