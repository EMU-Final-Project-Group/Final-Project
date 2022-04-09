using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapons : MonoBehaviour
{
    [Header("Weapon Screen")]
    public GameObject weaponScreen;
    public GameObject combatScreen;

    [Header("Weapon Selection")]
    public GameObject bat;
    public GameObject hatchet;
    public GameObject kitchenKnife;
    public GameObject butcherKnife;

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
                // Baseball bat
                bat.SetActive(true);
                hatchet.SetActive(false);
                kitchenKnife.SetActive(false);
                butcherKnife.SetActive(false);
                equippedWeapon = 1;
                break;
            case 2:
                // Kitchen Knife
                bat.SetActive(false);
                hatchet.SetActive(false);
                kitchenKnife.SetActive(true);
                butcherKnife.SetActive(false);
                equippedWeapon = 2;
                break;
            case 3:
                // Butcher Knife
                bat.SetActive(false);
                hatchet.SetActive(false);
                kitchenKnife.SetActive(false);
                butcherKnife.SetActive(true);
                equippedWeapon = 3;
                break;
            case 4:
                // Hatchet
                bat.SetActive(false);
                hatchet.SetActive(true);
                kitchenKnife.SetActive(false);
                butcherKnife.SetActive(false);
                equippedWeapon = 4;
                break;
            default:
                kitchenKnife.SetActive(true);
                equippedWeapon = 1;
                break;
        }
    }
}
