using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Targets the Player
    [Header("Player")]
    public GameObject player;

    // Distance to player
    public float distanceToTarget;
    public float interactDistance = 2.0f;
    public bool playerCloseEnough;
    public bool playerFartherEnough;

    // Update is called once per frame
    void Update()
    {
        // Measures the distance between the zombie and the player
        distanceToTarget = Vector3.Distance(player.transform.position, this.transform.position);
        PlayerDistanceCheck();
    }

    private void PlayerDistanceCheck()
    {
        if (distanceToTarget < interactDistance)
        {
            playerCloseEnough = true;
        }
        else
        {
            playerCloseEnough = false;
        }
    }
}
