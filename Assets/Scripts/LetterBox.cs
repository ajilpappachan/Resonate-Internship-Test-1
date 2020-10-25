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

    public char getLetter()
    {
        return letter;
    }

    public void goToAnswerSlot()
    {
        FindObjectOfType<AudioManager>().playButton();
        if(isAnswer)
        {
            Reload();
            gameController.addEmptySlot(currentSlot.GetComponent<AnswerSlot>().slotNumber);
            currentSlot.GetComponent<AnswerSlot>().showHint();
        }
        else
        {
            currentSlot = gameController.getCurrentAnswerSlot();
            gameObject.transform.position = currentSlot.transform.position;
            currentSlot.GetComponent<AnswerSlot>().setLetter(letter);
            currentSlot.GetComponent<AnswerSlot>().hideHint();
            isAnswer = true;

            if(letter == currentSlot.GetComponent<AnswerSlot>().getTargetLetter())
            {
                gameController.saveCorrectLetter();
            }
            else
            {
                gameController.saveIncorrectLetter();
            }
        }
        gameController.checkFullSlots();
    }

    public void useHint(GameObject slot)
    {
        gameObject.transform.position = slot.transform.position;
        if(isAnswer)
        {
            gameController.addEmptySlot(currentSlot.GetComponent<AnswerSlot>().slotNumber);
        }
        isAnswer = true;
        currentSlot = slot;
        currentSlot.GetComponent<AnswerSlot>().setLetter(letter);
        gameController.saveHint();
        gameController.checkFullSlots();
    }

    public void Reload()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = originalPosition;
        isAnswer = false;
    }
}
