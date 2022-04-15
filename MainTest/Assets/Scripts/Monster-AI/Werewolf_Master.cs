using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Werewolf_Master : MonoBehaviour
{
    // Targets the Player
    [Header("Chase Target")]
    public GameObject player;
    public GameObject monsterSelf;
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

    // Combat Variables
    [Header("Combat Stats")]
    private int healthPoints;

    private void Awake()
    {
        getPlayerStance = player.GetComponent<Locomotion>();
        animationManager = GetComponent<WerewolfAnimationManager>(); 
    }

    private void Start()
    {
        // Combat Setup
        healthPoints = 20;
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

        if(IsMonsterDead())
        {
            Debug.Log("THE MONSTER HAS BEEN DEFEATED");
            _agent.isStopped = true;
            StartCoroutine(DespawnMonster());
        }
        else
        {
            // Moves towards the player
            if (isAngered)
            {
                // isWalking = false;
                _agent.isStopped = false;
                moveSpeed = 3f;
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                _agent.SetDestination(player.transform.position);
                animationManager.UpdateAnimatorValues(0, 1.0f);
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

    public void MonsterTookDamage()
    {
        healthPoints--;
    }

    private bool IsMonsterDead()
    {
        if(healthPoints <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Despawns the monster after 30 seconds
    IEnumerator DespawnMonster()
    {
        yield return new WaitForSeconds(5);
        monsterSelf.SetActive(false);
    }
}
