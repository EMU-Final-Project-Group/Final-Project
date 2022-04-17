using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearbyDetection : MonoBehaviour
{
    // Targets the Player
    [Header("Player")]
    public GameObject player;

    // Text Object
    [Header("Floating Text")]
    public GameObject floatingText;

    // Distance to player
    public float distanceToTarget;

    // Distance to enable text
    public float enableDistance;
    public bool displayText;

    // Clue Found
    [Header("HUD Element")]
    public GameObject hudItem;
    public bool playerPickedUpClue;

    private void Start()
    {
        playerPickedUpClue = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Measures the distance between the zombie and the player
        distanceToTarget = Vector3.Distance(player.transform.position, this.transform.position);
        enableDistance = 2.0f;

        // Checks if the player is close enough to display the text
        SetTextDisplay();
        EnableTextObject();
    }

    private void SetTextDisplay()
    {
        if(PlayerDistanceCheck() && !playerPickedUpClue)
        {
            displayText = true;
        } 
        else
        {
            displayText = false;
        }
    }

    private void EnableTextObject()
    {
        if(displayText)
        {
            floatingText.SetActive(true);
        } 
        else
        {
            floatingText.SetActive(false);
        }
    }

    public bool PlayerDistanceCheck()
    {
        if(distanceToTarget <= enableDistance)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    public bool PlayerDistanceFarCheck()
    {
        var farDistance = enableDistance * 2;
        if(distanceToTarget <= farDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PickUpClue()
    {
        if(PlayerDistanceCheck())
        {
            playerPickedUpClue = true;
            // Debug.Log("Retrieved the clue");
            hudItem.SetActive(true);
        }
    }
}
