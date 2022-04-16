using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Objects for the weapons
    [Header("Weapons")]
    public GameObject knife;
    public GameObject butcher;
    public GameObject axe;
    public GameObject pitchFork;

    // Monster Enemies
    [Header("Enemies")]
    public GameObject werewolf;
    Werewolf_Master werewolfMasterScript;

    // Objects for the weapon colliders
    private Collider knifeCollider;
    private Collider butcherCollider;
    private Collider axeCollider;
    private Collider forkCollider;

    // Combat stats
    private bool availableToAttackAgain;
    private int currentWeapon;

    private void Awake()
    {
        // Gets the weapon box colliders
        knifeCollider = (BoxCollider)knife.GetComponent<Collider>();
        butcherCollider = (BoxCollider)butcher.GetComponent<Collider>();
        axeCollider = (BoxCollider)axe.GetComponent<Collider>();
        forkCollider = (BoxCollider)pitchFork.GetComponent<Collider>();

        // Gets the Monster Scripts
        werewolfMasterScript = werewolf.GetComponent<Werewolf_Master>();

        currentWeapon = 0;
    }

    private void Start()
    {
        // Initially disables the weapon colliders
        knifeCollider.enabled = false;
        butcherCollider.enabled = false;
        axeCollider.enabled = false;
        forkCollider.enabled = false;

        // Combat Stats
        availableToAttackAgain = true;
    }

    // Starts the attack process
    public void StartAttack(int weapon)
    {
        currentWeapon = weapon;

        if(availableToAttackAgain)
        {
            if (currentWeapon == 1)
            {
                AttackCommands(knifeCollider);
            }
            else if (currentWeapon == 2)
            {
                AttackCommands(butcherCollider);
            }
            else if (currentWeapon == 3)
            {
                AttackCommands(axeCollider);
            }
            else if (currentWeapon == 4)
            {
                AttackCommands(forkCollider);
            }
            else
            {
                Debug.Log("Uhh, I'll Punch");
            }
        }
        else
        {
            Debug.Log("Can't attack yet");
        }
    }

    private void AttackCommands(Collider weapon)
    {
        weapon.enabled = true;
        // Attack(weapon);
        OnTriggerEnter(weapon);
        StartCoroutine(DisableDelay(1, weapon));
    }

    // Detect the enemy
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("You have hit an enemy");
            werewolfMasterScript.MonsterTookDamage(1);
            availableToAttackAgain = false;
            StartCoroutine(PauseAttack(3));
        }
        else if(other.gameObject.tag == "Werewolf")
        {
            if(currentWeapon == 3)
            {
                Debug.Log("You hit with an Axe");
                werewolfMasterScript.MonsterTookDamage(3);
                availableToAttackAgain = false;
                StartCoroutine(PauseAttack(4));
            }
            else
            {
                Debug.Log("You hit with the wrong weapon");
                werewolfMasterScript.MonsterTookDamage(1);
                availableToAttackAgain = false;
                StartCoroutine(PauseAttack(2));
            }
        }
    }

    // Pauses x seconds before executing
    IEnumerator DisableDelay(int sec, Collider weapon)
    {
        yield return new WaitForSeconds(sec);
        weapon.enabled = false;
    }

    // Disables the ability to attack for X seconds
    IEnumerator PauseAttack(int pauseLength)
    {
        yield return new WaitForSeconds(pauseLength);
        availableToAttackAgain = true;
    }
}
