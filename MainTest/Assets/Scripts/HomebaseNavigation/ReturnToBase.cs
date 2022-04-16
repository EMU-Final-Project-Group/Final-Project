using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBase : MonoBehaviour
{
    // Gets the Player Detection script
    PlayerNearbyDetection playerDetection;

    // Gets the controls input
    ControllingPlayer playerControls;

    [Header("Game Objects")]
    public GameObject player;
    public GameObject homeBaseObject;
    HomeBaseInteractions homeBaseInterface;

    [Header("Monster Related Items")]
    public GameObject Monster;
    public GameObject defeatMonsterPrompt;
    ClueDisplayManager monsterClueDisplay;
    public bool LocalMonsterDefeated;

    [Header("Return Weapons")]
    public GameObject kitchenKnife;
    public GameObject butcherCleaver;
    public GameObject axe;
    public GameObject pitchFork;

    [Header("Set Weapon Status")]
    public GameObject urbanWeapon;
    ManageWeapons urbanEquippedWeapon;

    public GameObject homeBaseSpawn;
    private Vector3 homeBaseSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerDetection = GetComponent<PlayerNearbyDetection>();
        homeBaseInterface = homeBaseObject.GetComponent<HomeBaseInteractions>();
        monsterClueDisplay = Monster.GetComponent<ClueDisplayManager>();

        urbanEquippedWeapon = urbanWeapon.GetComponent<ManageWeapons>();

        homeBaseSpawnPoint = homeBaseSpawn.transform.position;

        defeatMonsterPrompt.SetActive(false);
        LocalMonsterDefeated = false;
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new ControllingPlayer();

            playerControls.MenuActions.Select.performed += i => ReturnToHomeBase();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void ReturnToHomeBase()
    {
        if(playerDetection.PlayerDistanceCheck())
        {
            if(LocalMonsterDefeated)
            {
                monsterClueDisplay.DisableAllDisplays();
                ReturnWeapons();
                player.transform.position = homeBaseSpawnPoint;
                homeBaseInterface.currentActiveMap = 0;
                homeBaseInterface.EnableHomebaseItems();
            }
            else
            {
                defeatMonsterPrompt.SetActive(true);
            }
        }
    }

    private void ReturnWeapons()
    {
        urbanEquippedWeapon.equippedWeapon = 0;
        kitchenKnife.SetActive(false);
        butcherCleaver.SetActive(false);
        axe.SetActive(false);
        pitchFork.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerDetection.PlayerDistanceCheck())
        {
            // prompt.SetActive(false);
            // DisableGuessScreen();
            // guessScreenOpen = false;
            defeatMonsterPrompt.SetActive(false);
        }
    }
}
