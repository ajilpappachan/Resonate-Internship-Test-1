using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    private char letter;
    private GameController gameController;
    //Recieve information about the letter
    public void Initialise(char letter, GameController gameController)
    {
        this.letter = letter;
        this.gameController = gameController;
    }

    public string getLetter()
    {
        return letter.ToString();
    }

    public void goToAnswerSlot()
    {
        gameObject.transform.position = gameController.getCurrentAnswerSlot().transform.position;
    }
}
