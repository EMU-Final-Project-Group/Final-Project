using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapons : MonoBehaviour
{
    [Header("Weapon Screen")]
    public GameObject weaponScreen;
    public GameObject combatScreen;
    public GameObject denyText;

    // Weapon Objects
    [Header("Weapon Selection")]
    public GameObject kitchenKnife;
    public GameObject butcherCleaver;
    public GameObject axe;
    public GameObject pitchFork;

    [Header("Weapon Boxes")]
    public GameObject urbanWeapons;
    public GameObject suburbWeapons;
    public GameObject map3Weapons;
    public GameObject map4Weapons;
    PlayerNearbyDetection urbanBoxDetection;
    PlayerNearbyDetection suburbBoxDetection;
    PlayerNearbyDetection map3BoxDetection;
    PlayerNearbyDetection map4BoxDetection;

    PlayerNearbyDetection playerDetection;
    public int equippedWeapon;
    public bool weaponScreenOpen;

    // Start is called before the first frame update
    void Awake()
    {
        playerDetection = GetComponent<PlayerNearbyDetection>();
        denyText.SetActive(false);
        equippedWeapon = 0;
        weaponScreenOpen = false;

        urbanBoxDetection = urbanWeapons.GetComponent<PlayerNearbyDetection>();
        suburbBoxDetection = suburbWeapons.GetComponent<PlayerNearbyDetection>();
        map3BoxDetection = map3Weapons.GetComponent<PlayerNearbyDetection>();
        map4BoxDetection = map4Weapons.GetComponent<PlayerNearbyDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerDetection.PlayerDistanceFarCheck())
        {
            denyText.SetActive(false);
        }
        DisableWeaponScreen();
    }

    public void HandleWeaponRack(int weaponSelection)
    {
        if(playerDetection.PlayerDistanceCheck())
        {
            if (combatScreen.activeSelf)
            {
                weaponScreen.SetActive(true);
                weaponScreenOpen = true;
                if (weaponSelection > 0)
                {
                    WeaponSelectionValue(weaponSelection);
                }
            }
            else
            {
                weaponScreenOpen = false;
                denyText.SetActive(true);
            }
        }
    }

    private void DisableWeaponScreen()
    {
        if(!urbanBoxDetection.PlayerDistanceCheck() && !suburbBoxDetection.PlayerDistanceCheck() && !map3BoxDetection.PlayerDistanceCheck() && !map4BoxDetection.PlayerDistanceCheck())
        {
            weaponScreen.SetActive(false);
        }
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
