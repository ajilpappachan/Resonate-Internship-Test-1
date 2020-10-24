using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSlot : MonoBehaviour
{
    public int slotNumber;
    public Vector2 position;

    public void Initialise(int slotNumber, Vector2 position)
    {
        this.slotNumber = slotNumber;
        this.position = position;
    }
}
