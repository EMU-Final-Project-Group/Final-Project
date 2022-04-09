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

    PlayerNearbyDetection playerDetection;

    // Start is called before the first frame update
    void Awake()
    {
        playerDetection = GetComponent<PlayerNearbyDetection>();
        DisableGuessScreen();

    }

    // Update is called once per frame
    void Update()
    {
        if(!playerDetection.PlayerDistanceFarCheck())
        {
            prompt.SetActive(false);
            DisableGuessScreen();
        }
    }

    public void HandleGuessMachine()
    {
        if(playerDetection.PlayerDistanceCheck())
        {
            if(CheckClueCollection())
            {
                guessScreen.SetActive(true);
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
}
