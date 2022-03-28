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
    int playerStance;

    // Animation
    ZombineAnimationManager animationManager;
    private float moveAmount;

    // AI System
    public NavMeshAgent _agent;

    // Wandering Variables
    public float moveSpeed;
    public float rotationSpeed = 100f;
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    void Awake()
    {
        playerScript = player.GetComponent<Locomotion>();
        animationManager = GetComponent<ZombineAnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Measures the distance between the zombie and the player
        distanceToTarget = Vector3.Distance(player.transform.position, this.transform.position);

        // Gets the player stance
        playerStance = playerScript.currentStance;
        HandlePlayerStance();

        // Criteria for zombie to engage player
        EngagePlayer();

        // Moves the AI to the player 
        if(isAngered)
        {
            //_agent.isStopped = false;
            moveSpeed = 2.0f;
            _agent.SetDestination(player.transform.position);
            animationManager.UpdateAnimatorValues(0, 1);
        }
        else
        {
            moveSpeed = 0.5f;
            //animationManager.UpdateAnimatorValues(0, 0.5f);
            //_agent.isStopped = true;
            WanderingMode();
        }
    }

    private void HandlePlayerStance()
    {
        // Checks the player stance for the engage distance
        if (playerStance == 2)
        {
            engageDistance = 20;
        }
        else if (playerStance == 0)
        {
            engageDistance = 5;
        }
        else
        {
            engageDistance = 10;
        }
    }

    private void EngagePlayer()
    {
        if(distanceToTarget <= engageDistance)
        {
            isAngered = true;
        } else
        {
            isAngered = false;
        }
    }

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
}
