using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBox : MonoBehaviour
{
    private bool isAnswer;
    private char letter;
    private GameController gameController;
    private Vector3 originalPosition;
    private GameObject currentSlot;

    //Recieve information about the letter
    public void Initialise(char letter, GameController gameController)
    {
        this.letter = letter;
        this.gameController = gameController;
        originalPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        isAnswer = false;
    }

    public string getLetter()
    {
        return letter.ToString();
    }

    public void goToAnswerSlot()
    {
        if(isAnswer)
        {
            Reload();
            gameController.addEmptySlot(currentSlot.GetComponent<AnswerSlot>().slotNumber);
        }
        else
        {
            currentSlot = gameController.getCurrentAnswerSlot();
            gameObject.transform.position = currentSlot.transform.position;
            isAnswer = true;
        }
        gameController.checkFullSlots();
    }

    public void Reload()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = originalPosition;
        isAnswer = false;
    }
}
