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

    // Monster Items
    [Header("Main Monster Items")]
    public GameObject mainMonster;
    MainMonsterManager mainMonsterManager;

    // Distance to the Target
    [Header("Distance Values")]
    public float distanceToTarget;
    public float engageDistance;

    // Enage Status
    [Header("Engage Status")]
    public bool isAngered;
    int playerStance;

    // Animation
    public Animator animator;
    WerewolfAnimationManager animationManager;

    // AI Navigation System
    public NavMeshAgent _agent;

    // Wandering Variables
    [Header("Wandering Values")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 200f;

    // Combat Variables
    [Header("Combat Stats")]
    private int currentHealth;
    private bool availableToAttackAgain;
    public bool werewolfIsDefeated;

    private void Awake()
    {
        getPlayerStance = player.GetComponent<Locomotion>();
        animationManager = GetComponent<WerewolfAnimationManager>();
        mainMonsterManager = mainMonster.GetComponent<MainMonsterManager>();
        availableToAttackAgain = true;
    }

    private void Start()
    {
        // Combat Setup
        currentHealth = 15;
        werewolfIsDefeated = false;
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
            _agent.isStopped = true;
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
                if(distanceToTarget < 2 && availableToAttackAgain)
                {
                    animator.SetTrigger("monsterAttack");
                    availableToAttackAgain = false;
                    mainMonsterManager.AddMadness(100);
                    StartCoroutine(PauseAttack(5));
                }
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

    public void MonsterTookDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (IsMonsterDead())
        {
            Debug.Log("THE MONSTER HAS BEEN DEFEATED");
            werewolfIsDefeated = true;
            animator.SetTrigger("deathTrigger");
            StartCoroutine(DespawnMonster());
        }
    }

    private bool IsMonsterDead()
    {
        if(currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Disables the ability to attack for X seconds
    IEnumerator PauseAttack(int pauseLength)
    {
        yield return new WaitForSeconds(pauseLength);
        availableToAttackAgain = true;
    }

    // Despawns the monster after 30 seconds
    IEnumerator DespawnMonster()
    {
        yield return new WaitForSeconds(30);
        monsterSelf.SetActive(false);
    }
}
