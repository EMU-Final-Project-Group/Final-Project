using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessMachine : MonoBehaviour
{
    // [Header("Guessing Box Object")]
    // public GameObject guessMachine;

    [Header("Clues Collected")]
    public GameObject clue1;
    public GameObject clue2;
    public GameObject clue3;
    public GameObject clue4;

    [Header("More Clues Prompt")]
    public GameObject prompt;

    [Header("Guess Screen")]
    public GameObject guessScreen;

    [Header("UI Element Toggles")]
    public GameObject clueCollection;
    public GameObject combatCollection;

    PlayerNearbyDetection playerDetection;
    public bool guessScreenOpen;

    // Start is called before the first frame update
    void Awake()
    {
        playerDetection = GetComponent<PlayerNearbyDetection>();
        DisableGuessScreen();
        guessScreenOpen = false;
        clueCollection.SetActive(true);
        combatCollection.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerDetection.PlayerDistanceFarCheck())
        {
            prompt.SetActive(false);
            DisableGuessScreen();
            guessScreenOpen = false;
        }
    }

    public void HandleGuessMachine()
    {
        if(playerDetection.PlayerDistanceCheck())
        {
            if(CheckClueCollection())
            {
                guessScreen.SetActive(true);
                guessScreenOpen = true;
            }
        }
    }

    private bool CheckClueCollection()
    {
        if(clue1.activeSelf && clue2.activeSelf && clue3.activeSelf & clue4.activeSelf)
        {
            return true;
        }
        else
        {
            prompt.SetActive(true);
            return false;
        }
    }

    private void DisableGuessScreen()
    {
        guessScreen.SetActive(false);
    }

    public void PlayerGuessSubmission(int guess)
    {
        if(CheckClueCollection())
        {
            if (guess == 1)
            {
                Debug.Log("Winner is you.");
                clueCollection.SetActive(false);
                combatCollection.SetActive(true);
                DisableGuessScreen();
            }
            else
            {
                Debug.Log("Do not pass go, do not collect $200");
            }
        }
    }
}
