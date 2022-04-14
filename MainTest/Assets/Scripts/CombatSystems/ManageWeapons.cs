using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapons : MonoBehaviour
{
    [Header("Weapon Screen")]
    public GameObject weaponScreen;
    public GameObject combatScreen;

    // Weapon Objects
    [Header("Weapon Selection")]
    public GameObject kitchenKnife;
    public GameObject butcherCleaver;
    public GameObject axe;
    public GameObject pitchFork;

    PlayerNearbyDetection playerDetection;
    public bool weaponScreenOpen;
    public int equippedWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        playerDetection = GetComponent<PlayerNearbyDetection>();
        DisableWeaponScreen();
        weaponScreenOpen = false;
        equippedWeapon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerDetection.PlayerDistanceFarCheck())
        {
            DisableWeaponScreen();
            weaponScreenOpen=false;
        }
    }

    public void HandleWeaponRack(int weaponSelection)
    {
        if(playerDetection.PlayerDistanceCheck())
        {
            if (combatScreen.activeSelf)
            {
                weaponScreen.SetActive(true);
                weaponScreenOpen = true;
                if(weaponSelection > 0)
                {
                    WeaponSelectionValue(weaponSelection);
                }
            }
        }
    }

    private void DisableWeaponScreen()
    {
        weaponScreen.SetActive(false);
    }

    public void WeaponSelectionValue(int wepNum)
    {
        switch (wepNum)
        {
            case 1:
                // Kitchen Knife
                kitchenKnife.SetActive(true);
                butcherCleaver.SetActive(false);
                axe.SetActive(false);
                pitchFork.SetActive(false);
                equippedWeapon = 1;
                break;
            case 2:
                // Butcher Cleaver
                kitchenKnife.SetActive(false);
                butcherCleaver.SetActive(true);
                axe.SetActive(false);
                pitchFork.SetActive(false);
                equippedWeapon = 2;
                break;
            case 3:
                // Axe
                kitchenKnife.SetActive(false);
                butcherCleaver.SetActive(false);
                axe.SetActive(true);
                pitchFork.SetActive(false);
                equippedWeapon = 3;
                break;
            case 4:
                // PitchFork
                kitchenKnife.SetActive(false);
                butcherCleaver.SetActive(false);
                axe.SetActive(false);
                pitchFork.SetActive(true);
                equippedWeapon = 4;
                break;
            default:
                kitchenKnife.SetActive(true);
                equippedWeapon = 1;
                break;
        }
    }
}
