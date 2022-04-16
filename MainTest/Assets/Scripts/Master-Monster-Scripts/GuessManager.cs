using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    [Header("Warning Display")]
    public GameObject popUpTip;
    public GameObject guessScreen;
    public GameObject combatScreen;
    public GameObject monsterMaster;
    ClueDisplayManager monsterClueDisplay;

    private GameObject clue1;
    private GameObject clue2;
    private GameObject clue3;
    private GameObject clue4;

    PlayerNearbyDetection playerDetection;

    PlayerNearbyDetection clue1Detection;
    PlayerNearbyDetection clue2Detection;
    PlayerNearbyDetection clue3Detection;
    PlayerNearbyDetection clue4Detection;

    public bool spawnMonster;
    private bool isWerewolf;
    private bool isVampire;
    private bool isWitch;
    private bool isDemon;

    private void Awake()
    {
        playerDetection = GetComponent<PlayerNearbyDetection>();
    }

    private void Start()
    {
        popUpTip.SetActive(false);
        spawnMonster = false;
        guessScreen.SetActive(false);
        combatScreen.SetActive(false);

        monsterClueDisplay = monsterMaster.GetComponent<ClueDisplayManager>();
    }

    private void Update()
    {
        if(!playerDetection.PlayerDistanceFarCheck())
        {
            popUpTip.SetActive(false);
        }
    }

    public void SetClueObjects(int monsterType, GameObject item1, GameObject item2, GameObject item3, GameObject item4)
    {
        clue1 = item1;
        clue2 = item2;
        clue3 = item3;
        clue4 = item4;
        clue1Detection = clue1.GetComponent<PlayerNearbyDetection>();
        clue2Detection = clue2.GetComponent<PlayerNearbyDetection>();
        clue3Detection = clue3.GetComponent<PlayerNearbyDetection>();
        clue4Detection = clue4.GetComponent<PlayerNearbyDetection>();

        if(monsterType == 1)
        {
            isWerewolf = true;
            isVampire = false;
            isWitch = false;
            isDemon = false;
        }
        else if(monsterType == 2)
        {
            isWerewolf = false;
            isVampire = true;
            isWitch = false;
            isDemon = false;
        }
        else if(monsterType == 3)
        {
            isWerewolf = false;
            isVampire = false;
            isWitch = true;
            isDemon = false;
        }
        else if(monsterType == 4)
        {
            isWerewolf = false;
            isVampire = false;
            isWitch = false;
            isDemon = true;
        }
    }

    public void HandleGuessInteraction()
    {
        if(playerDetection.PlayerDistanceCheck())
        {
            if(CheckIfAllCluesCollected())
            {
                guessScreen.SetActive(true);
            }
            else
            {
                DisplayTip();
            }
        }
    }

    public void PlayerGuessSubmission(int guessValue)
    {
        Debug.Log("My new Player guess is: " + guessValue);
        if(isWerewolf && guessValue == 1)
        {
            Debug.Log("Correct Guess");
            spawnMonster = true;
            CorrectGuess();
        }
        else if(isVampire && guessValue == 2)
        {
            Debug.Log("Correct Guess");
            spawnMonster = true;
            CorrectGuess();
        }
        else if(isWitch && guessValue == 3)
        {
            Debug.Log("Correct Guess");
            spawnMonster = true;
            CorrectGuess();
        }
        else if(isDemon && guessValue == 4)
        {
            Debug.Log("Correct Guess");
            spawnMonster = true;
            CorrectGuess();
        }
        else
        {
            Debug.Log("WRONG");
        }
    }

    private void CorrectGuess()
    {
        guessScreen.SetActive(false);
        combatScreen.SetActive(true);
        monsterClueDisplay.DisableAllDisplays();
    }

    public bool CheckIfAllCluesCollected()
    {
        if(clue1Detection.playerPickedUpClue && clue2Detection.playerPickedUpClue && clue3Detection.playerPickedUpClue && clue4Detection.playerPickedUpClue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DisplayTip()
    {
        popUpTip.SetActive(true);
    }
}
