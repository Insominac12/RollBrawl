using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public GameObject leftDice;
    public GameObject rightDice;

    public GameObject diceZone; 

    public Sprite[] typeDice;

    public TextMeshProUGUI result;

    public int resultDices;

    public GameObject button;

    public void RollDices()
    {
        Animator animLeftDice = leftDice.GetComponent<Animator>();
        Animator animRightDice = rightDice.GetComponent<Animator>();

        result.gameObject.SetActive(false);
        button.gameObject.SetActive(false);

        animLeftDice.enabled = true;
        animRightDice.enabled = true;

        animLeftDice.Play("Dice - Anim");
        animRightDice.Play("Dice - Anim");

        Invoke("Dices", 5f);
    }

    public void Dices()
    {
        Animator animLeftDice = leftDice.GetComponent<Animator>();
        Animator animRightDice = rightDice.GetComponent<Animator>();

        animLeftDice.enabled = false;
        animRightDice.enabled = false;

        int randomLeftDice = Random.Range(0, 6);
        int randomRightDice = Random.Range(0, 6);

        Debug.Log("Au picat numerele" + (randomLeftDice + 1) + ", " + (randomRightDice + 1));

        leftDice.GetComponent<Image>().sprite = typeDice[randomLeftDice];
        rightDice.GetComponent<Image>().sprite = typeDice[randomRightDice];

        result.gameObject.SetActive(true);
        result.text = "Ai dat un " + (randomLeftDice + 1 + randomRightDice + 1) + "!";

        resultDices = randomLeftDice + 1 + randomRightDice + 1;
    }
}
