using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Weapon Selection")]
    public GameObject bat;
    public GameObject hatchet;
    public GameObject kitchenKnife;
    public GameObject butcherKnife;

    public int equipWeapon;

    private void Awake()
    {
        bat.SetActive(false);
        hatchet.SetActive(false);
        kitchenKnife.SetActive(false);
        butcherKnife.SetActive(false);
    }

    public void WeaponSelectionValue(int wepNum)
    {
        switch (wepNum)
        {
            case 1:
                bat.SetActive(true);
                hatchet.SetActive(false);
                kitchenKnife.SetActive(false);
                butcherKnife.SetActive(false);
                break;
            case 2:
                bat.SetActive(false);
                hatchet.SetActive(true);
                kitchenKnife.SetActive(false);
                butcherKnife.SetActive(false);
                break;
            case 3:
                bat.SetActive(false);
                hatchet.SetActive(false);
                kitchenKnife.SetActive(true);
                butcherKnife.SetActive(false);
                break;
            case 4:
                bat.SetActive(false);
                hatchet.SetActive(false);
                kitchenKnife.SetActive(false);
                butcherKnife.SetActive(true);
                break;
            default:
                kitchenKnife.SetActive(true);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
