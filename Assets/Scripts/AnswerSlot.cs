using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSlot : MonoBehaviour
{
    public int slotNumber;
    public Vector2 position;
    private char letter;
    private GameObject letterBox;
    private GameObject hint;
    private bool usedHint;
    private GameController gameController;

    private GameObject starsUI;
    public GameObject star;

    public void Initialise(int slotNumber, Vector2 position, GameObject letterBox, GameController gameController)
    {
        this.slotNumber = slotNumber;
        this.position = position;
        this.letterBox = letterBox;
        this.gameController = gameController;
        hint = gameObject.transform.GetChild(0).gameObject;
        usedHint = false;
        showHint();
    }

    public char getTargetLetter()
    {
        return letterBox.GetComponent<LetterBox>().getLetter();
    }

    public void setLetter(char letter)
    {
        this.letter = letter;
    }

    public string getletter()
    {
        return letter.ToString();
    }

    public void giveStar()
    {
        if(!usedHint)
        {
            starsUI = GameObject.FindGameObjectWithTag("StarsUI");
            Instantiate(star, starsUI.transform);
        }
    }

    IEnumerator Hint()
    {
        yield return new WaitForSeconds(5.0f);
        hint.SetActive(true);
        StopCoroutine("Hint");
    }

    public void showHint()
    {
        StartCoroutine("Hint");
    }

    public void hideHint()
    {
        StopCoroutine("Hint");
        hint.SetActive(false);
    }

    public void useHint()
    {
        hint.SetActive(false);
        gameController.removeSlot(slotNumber);
        usedHint = true;
        letterBox.GetComponent<LetterBox>().useHint(gameObject);
    }
}
