using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_Master : MonoBehaviour
{
    // Targets the Player
    public GameObject player;
    Locomotion playerScript;

    // Distance to the target
    public float distanceToTarget;
    public float engageDistance;

    // Checks if the enemy will chase
    public bool isAngered;

    // AI System
    public NavMeshAgent _agent;

    // Start is called before the first frame update
    void Awake()
    {
        playerScript = player.GetComponent<Locomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        // Measures the distance between the zombie and the player
        distanceToTarget = Vector3.Distance(player.transform.position, this.transform.position);

        // Gets the player stance
        int playerStance = playerScript.currentStance;

        // Checks the player stance for the engage distance
        if(playerStance == 2)
        {
            // Debug.Log("SPRINTING, ENGAGE");
            engageDistance = 20;
        } 
        else if (playerStance == 0)
        {
            engageDistance = 5;
            // Debug.Log("SNEAK, ENGAGE");
        }
        else
        {
            engageDistance = 10;
        }

        // Criteria to engage enemy
        if(distanceToTarget <= engageDistance)
        {
            // Debug.Log("Regular, ENGAGE");
            isAngered = true;
        }
        if(distanceToTarget > engageDistance)
        {
            // Debug.Log("STOP");
            isAngered = false;
        }

        // Moves the AI to the player 
        if(isAngered)
        {
            _agent.isStopped = false;
            _agent.SetDestination(player.transform.position);
        }
        else
        {
            _agent.isStopped = true;
        }
    }
}
