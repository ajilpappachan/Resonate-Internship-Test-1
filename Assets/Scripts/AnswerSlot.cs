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
    public GameObject starSlot;

    //Initialise Answer Slot
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

    //Get the target Letter of this Answer Slot
    public char getTargetLetter()
    {
        return letterBox.GetComponent<LetterBox>().getLetter();
    }

    //Set the currently added letter
    public void setLetter(char letter)
    {
        this.letter = letter;
    }

    //Get the current Letter
    public string getletter()
    {
        return letter.ToString();
    }

    //Award Star for the current Letter
    public void giveStar()
    {
        if(!usedHint)
        {
            starsUI = GameObject.FindGameObjectWithTag("StarsUI");
            GameObject spawnedStar = Instantiate(star, gameObject.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("UICanvas").transform);
            GameObject starSlot = Instantiate(this.starSlot, starsUI.transform);
            LeanTween.move(spawnedStar, starSlot.transform, 1.0f).setEase(LeanTweenType.easeSpring);
        }
        FindObjectOfType<AudioManager>().playStar();
    }

    //Show and Hide Hints
    IEnumerator Hint()
    {
        yield return new WaitForSeconds(30.0f);
        hint.SetActive(true);
        gameController.setHintActive(true);
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
        gameController.setHintActive(false);
        gameController.removeSlot(slotNumber);
        usedHint = true;
        letterBox.GetComponent<LetterBox>().useHint(gameObject);
    }
}
