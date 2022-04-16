using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessManager : MonoBehaviour
{
    [Header("Warning Display")]
    public GameObject popUpTip;
    public GameObject guessScreen;

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

        if(monsterType == 0)
        {
            isWerewolf = true;
            isVampire = false;
            isWitch = false;
            isDemon = false;
            Debug.Log("Monster clue " + clue1 + " should be Werewolf");
        }
        else if(monsterType == 1)
        {
            isWerewolf = false;
            isVampire = true;
            isWitch = false;
            isDemon = false;
            Debug.Log("Monster clue " + clue1 + " should be Vampire");
        }
        else if(monsterType == 2)
        {
            isWerewolf = false;
            isVampire = false;
            isWitch = true;
            isDemon = false;
            Debug.Log("Monster clue " + clue1 + " should be Witch");
        }
        else if(monsterType == 3)
        {
            isWerewolf = false;
            isVampire = false;
            isWitch = false;
            isDemon = true;
            Debug.Log("Monster clue " + clue1 + " should be Demon");
        }
    }

    public void HandleGuessInteraction()
    {
        if(playerDetection.PlayerDistanceCheck())
        {
            if(CheckIfAllCluesCollected())
            {
                guessScreen.SetActive(true);
                if(isWerewolf)
                {
                    Debug.Log("Answer is Werewolf");
                }
                else if(isVampire)
                {
                    Debug.Log("Answer is Vampire");
                }
                else if(isWitch)
                {
                    Debug.Log("Answer is Witch");
                }
                else if(isDemon)
                {
                    Debug.Log("Answer is Demon");
                }
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
        }
        else if(isVampire && guessValue == 2)
        {
            Debug.Log("Correct Guess");
            spawnMonster = true;
        }
        else if(isWitch && guessValue == 3)
        {
            Debug.Log("Correct Guess");
            spawnMonster = true;
        }
        else if(isDemon && guessValue == 4)
        {
            Debug.Log("Correct Guess");
            spawnMonster = true;
        }
        else
        {
            Debug.Log("Incorrect Guess");
            Debug.Log("Correct num: " + guessValue);
            if (isWerewolf)
            {
                Debug.Log("Real Answer is Werewolf");
            }
            else if (isVampire)
            {
                Debug.Log("Real Answer is Vampire");
            }
            else if (isWitch)
            {
                Debug.Log("Real Answer is Witch");
            }
            else if (isDemon)
            {
                Debug.Log("Real Answer is Demon");
            }
        }

        /*
        if(guessValue == correctMonster)
        {
            
        }
        else
        {
            Debug.Log("Incorrect Guess");
        }
        */
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
