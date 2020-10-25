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

    //Get the letter from this gameobject
    public char getLetter()
    {
        return letter;
    }

    //Go to a an answer slot or go back to game board
    public void goToAnswerSlot()
    {
        //Play Button Audio
        FindObjectOfType<AudioManager>().playButton();
        if(isAnswer)
        {
            //Go back to game board
            Reload();
            gameController.addEmptySlot(currentSlot.GetComponent<AnswerSlot>().slotNumber);
            currentSlot.GetComponent<AnswerSlot>().showHint();
        }
        else
        {
            //Go to answer slot
            currentSlot = gameController.getCurrentAnswerSlot();
            gameObject.transform.position = currentSlot.transform.position;
            currentSlot.GetComponent<AnswerSlot>().setLetter(letter);
            currentSlot.GetComponent<AnswerSlot>().hideHint();
            isAnswer = true;

            //Update Save Record
            if(letter == currentSlot.GetComponent<AnswerSlot>().getTargetLetter())
            {
                gameController.saveCorrectLetter();
            }
            else
            {
                gameController.saveIncorrectLetter();
            }
        }
        //Check if all slots are full
        gameController.checkFullSlots();
    }

    //Go to answer if hint is used and save the hint record
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

    //Go back to starting state
    public void Reload()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = originalPosition;
        isAnswer = false;
    }
}
