using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Werewolf_Master : MonoBehaviour
{
    // Targets the Player
    [Header("Chase Target")]
    public GameObject player;
    Locomotion getPlayerStance;

    // Distance to the Target
    [Header("Distance Values")]
    public float distanceToTarget;
    public float engageDistance;

    // Enage Status
    [Header("Engage Status")]
    public bool isAngered;
    int playerStance;

    // Animation
    WerewolfAnimationManager animationManager;

    // AI Navigation System
    public NavMeshAgent _agent;

    
    // Wandering Variables
    [Header("Wandering Values")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 200f;

    /*
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    */

    private void Awake()
    {
        getPlayerStance = player.GetComponent<Locomotion>();
        animationManager = GetComponent<WerewolfAnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the distance to target
        DistanceToTarget();

        // Gets the Player's stance
        playerStance = getPlayerStance.currentStance;
        SetEngageDistance();

        // Criteria to engage the player
        EngagePlayer();

        // Moves towards the player
        if(isAngered)
        {
            // isWalking = false;
            _agent.isStopped = false;
            moveSpeed = 3f;
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            _agent.SetDestination(player.transform.position);
            animationManager.UpdateAnimatorValues(0,1.0f);
        }
        else
        {
            // isWalking=true;
            moveSpeed = 1.5f;
            animationManager.UpdateAnimatorValues(0, 0.5f);
            _agent.isStopped = true;
            // WanderingMode();
        }
    }

    private void DistanceToTarget()
    {
        distanceToTarget = Vector3.Distance(player.transform.position, this.transform.position);
    }

    private void SetEngageDistance()
    {
        if(playerStance == 2)
        {
            engageDistance = 40;
        }
        else if(playerStance == 0)
        {
            engageDistance = 15;
        }
        else {
            engageDistance = 30;
        }
    }

    private void EngagePlayer()
    {
        if(distanceToTarget <= engageDistance)
        {
            isAngered = true;
        } 
        else
        {
            isAngered = false;
        }
    }

    /*
    private void WanderingMode()
    {
        if(!isWandering)
        {
            StartCoroutine(Wander());
        }
        if(isRotatingRight)
        {
            animationManager.UpdateAnimatorValues(0, 0);
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
        }
        if(isRotatingLeft)
        {
            animationManager.UpdateAnimatorValues(0, 0);
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
        }
        if(isWalking)
        {
            animationManager.UpdateAnimatorValues(0, 0.5f);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 5);
        int walkTime = Random.Range(1, 6);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }
    */
}
