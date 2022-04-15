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

    // Objects for the weapon colliders
    private Collider knifeCollider;
    private Collider butcherCollider;
    private Collider axeCollider;
    private Collider forkCollider;

    private void Awake()
    {
        // Gets the weapon box colliders
        knifeCollider = (BoxCollider)knife.GetComponent<Collider>();
        butcherCollider = (BoxCollider)butcher.GetComponent<Collider>();
        axeCollider = (BoxCollider)axe.GetComponent<Collider>();
        forkCollider = (BoxCollider)pitchFork.GetComponent<Collider>();

        // Initially disables the weapon colliders
        knifeCollider.enabled = false;
        butcherCollider.enabled = false;
        axeCollider.enabled = false;
        forkCollider.enabled = false;
    }

    // Starts the attack process
    public void StartAttack(int currentWeapon)
    {
        if(currentWeapon == 1)
        {
            AttackCommands(knifeCollider);
        }
        else if(currentWeapon == 2)
        {
            AttackCommands(butcherCollider);
        }
        else if(currentWeapon == 3)
        {
            AttackCommands(axeCollider);
        } 
        else if(currentWeapon == 4)
        {
            AttackCommands(forkCollider);
        }
        else
        {
            Debug.Log("Uhh");
        }
    }

    private void AttackCommands(Collider weapon)
    {
        weapon.enabled = true;
        // Attack(weapon);
        OnTriggerEnter(weapon);
        StartCoroutine(DisableDelay(1, weapon));
    }

    /*
    private void Attack(Collider weapon)
    {
        // Detect Enemy
        OnTriggerEnter(weapon);

        // Damage the enemies

    }
    */

    // Detect the enemy
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("You have hit the werewolf with");
        }
    }

    // Pauses x seconds before executing
    IEnumerator DisableDelay(int sec, Collider weapon)
    {
        yield return new WaitForSeconds(sec);
        weapon.enabled = false;
    }
}
