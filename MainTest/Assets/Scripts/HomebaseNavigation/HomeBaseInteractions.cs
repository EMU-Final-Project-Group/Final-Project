using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeBaseInteractions : MonoBehaviour
{
    // Gets the controls input
    ControllingPlayer playerControls;
    PlayerAccessoryManagement playerAccessoryManagement;

    [Header("Player")]
    public GameObject player;
    public int currentActiveMap;

    [Header("Bathroom Objects")]
    public GameObject bathroomDoorClosed;
    public GameObject bathroomDoorOpen;
    public GameObject toiletCoverClosed;
    public GameObject toiletCoverOpen;

    [Header("Controller Instructions")]
    public GameObject controllerPrompt;

    [Header("Map Selection Display")]
    public GameObject mapDetectionCube;
    public GameObject mapDisplayToggle;
    public GameObject mapPrompt;
    public GameObject mapStartText;
    public Button urbanButton;
    public Button suburbButton;
    public Button graveyardButton;
    public Button tbdButton;

    [Header("Gender Option Display")]
    public GameObject genderDetectionCube;
    public GameObject genderDisplayToggle;
    public GameObject genderDisplayPrompt;
    public GameObject genderSelectText;
    public Button maleButton;
    public Button femaleButton;

    [Header("Player Gender Choices")]
    public GameObject maleModel;
    public GameObject femaleModel;

    [Header("Player Accessory Choice")]
    public GameObject accDetectionCube;
    public GameObject accDisplayToggle;
    public GameObject accDisplayPrompt;
    public GameObject accDPadSelect;
    public GameObject accSelectText;

    [Header("Map Spawn Points")]
    public GameObject urbanSpawn;
    private Vector3 urbanSpawnPoint;
    public GameObject suburbSpawn;
    private Vector3 suburbSpawnPoint;
    public GameObject map3Spawn;
    private Vector3 map3SpawnPoint;
    public GameObject map4Spawn;
    private Vector3 map4SpawnPoint;

    [Header("UI Elements to Disable in Homebase")]
    public GameObject clueScreenDisable;
    public GameObject combatScreenDisable;

    [Header("Homebase Elements to Disable when leaving homebase")]
    public GameObject controlsPanel;
    public GameObject genderPanel;
    public GameObject accessoriesPanel;
    public GameObject mapPanel;

    [Header("Monster Items")]
    public GameObject monsterManager;
    MainMonsterManager mainMonsterManager;
    ClueDisplayManager clueDisplayManager;

    // Player Bathroom Interaction Items
    PlayerInteraction doorInteractionClosed;
    PlayerInteraction doorInteractionOpen;
    PlayerInteraction toiletClosed;
    PlayerInteraction toiletOpen;

    // Player Map Interactive Items
    PlayerInteraction mapSelectionDistanceCheck;
    PlayerInteraction genderSelectionDistanceCheck;
    PlayerInteraction accSelectionDistanceCheck;

    // Distance to player
    public float distanceToTarget;

    // Map Starting Selections
    private int mapCurrentlySelected;
    private int genderCurrentlySelected;
    private int accCursonLocation;
    private int monstersDefeated;
    private int playMessageOnce;

    private void Start()
    {
        playerAccessoryManagement = GetComponent<PlayerAccessoryManagement>();
        controllerPrompt.SetActive(true);
        currentActiveMap = 0;
        monstersDefeated = 0;
        playMessageOnce = 1;

        #region Bathroom Items
        // Gets the Bathroom player detection Items
        doorInteractionClosed = bathroomDoorClosed.GetComponent<PlayerInteraction>();
        doorInteractionOpen = bathroomDoorOpen.GetComponent<PlayerInteraction>();
        toiletClosed = toiletCoverClosed.GetComponent<PlayerInteraction>();
        toiletOpen = toiletCoverOpen.GetComponent<PlayerInteraction>();

        // Sets the starting status of items
        bathroomDoorClosed.SetActive(true);
        bathroomDoorOpen.SetActive(false);
        toiletCoverClosed.SetActive(true);
        toiletCoverOpen.SetActive(false);
        #endregion

        #region Map Selection Items
        // Turns off the map display
        mapDisplayToggle.SetActive(false);
        mapStartText.SetActive(true);
        mapPrompt.SetActive(true );
        mapCurrentlySelected = 1;

        mapSelectionDistanceCheck = mapDetectionCube.GetComponent<PlayerInteraction>();

        // Sets the map button colors
        urbanButton.GetComponent<Image>().color = Color.red;
        suburbButton.GetComponent<Image>().color = Color.white;
        graveyardButton.GetComponent<Image>().color = Color.white;
        tbdButton.GetComponent<Image>().color = Color.white;
        #endregion

        #region Gender Selection Items
        // Turns off the gender display
        genderDisplayToggle.SetActive(false);
        genderSelectText.SetActive(true);
        genderDisplayPrompt.SetActive(true);

        genderSelectionDistanceCheck = genderDetectionCube.GetComponent<PlayerInteraction>();
        genderCurrentlySelected = 1;

        maleButton.GetComponent<Image>().color = Color.red;
        femaleButton.GetComponent<Image>().color = Color.white;
        maleModel.SetActive(true);
        femaleModel.SetActive(false);
        #endregion

        #region Accessory Selection Items
        // Turns off the acc display
        accDisplayToggle.SetActive(false);
        accSelectText.SetActive(false);
        accDPadSelect.SetActive(false);
        accDisplayPrompt.SetActive(true);

        accSelectionDistanceCheck = accDetectionCube.GetComponent<PlayerInteraction>();
        accCursonLocation = 1;
        #endregion

        #region Disable Game UI Items
        clueScreenDisable.SetActive(false);
        #endregion

        #region Spawn Coordinates
        urbanSpawnPoint = urbanSpawn.transform.position;
        suburbSpawnPoint = suburbSpawn.transform.position;
        map3SpawnPoint = map3Spawn.transform.position;
        map4SpawnPoint = map4Spawn.transform.position;
        #endregion

        #region Get Monster Scripts
        mainMonsterManager = monsterManager.GetComponent<MainMonsterManager>();
        clueDisplayManager = monsterManager.GetComponent<ClueDisplayManager>();
        clueDisplayManager.DisableAllDisplays();
        #endregion
    }

    private void Update()
    {
        // Map Display Turn Off
        if(!mapSelectionDistanceCheck.playerCloseEnough)
        {
            mapDisplayToggle.SetActive(false);
            mapStartText.SetActive(false);
        }

        // Gender Display Turn off
        if(!genderSelectionDistanceCheck.playerCloseEnough)
        {
            genderDisplayToggle.SetActive(false);
            genderSelectText.SetActive(false);
            genderDisplayPrompt.SetActive(true);
        }

        // Accessory Display Turn Off
        if(!accSelectionDistanceCheck.playerCloseEnough)
        {
            accDisplayToggle.SetActive(false);
            accSelectText.SetActive(false);
            accDPadSelect.SetActive(false);
            accDisplayPrompt.SetActive(true);
        }

        // Turns off prompt when leaving home base
        if(mapSelectionDistanceCheck.playerFartherEnough)
        {
            mapPrompt.SetActive(false);
            accDisplayPrompt.SetActive(false);
            controllerPrompt.SetActive(false);
            genderDisplayPrompt.SetActive(false);
        }

        if(monstersDefeated == 4 && playMessageOnce == 1)
        {
            Debug.Log("YOU WIN THE GAME");
            playMessageOnce--;
        }
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new ControllingPlayer();

            // Pressing the X button
            playerControls.PlayerAction.ObjectInteract.performed += i => HandleObjectInteraction();

            // Pressing the Y button
            playerControls.MenuActions.SelectWithY.performed += i => HandleMenuSelection();

            // D Pad directions
            playerControls.MenuActions.NavUp.performed += i => HandleDPadPress(1);
            playerControls.MenuActions.NavDown.performed += i => HandleDPadPress(2);
            playerControls.MenuActions.NavLeft.performed += i => HandleDPadPress(3);
            playerControls.MenuActions.NavRight.performed += i => HandleDPadPress(4);
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleObjectInteraction()
    {
        // Interact with the bathroom door
        if(doorInteractionClosed.playerCloseEnough || doorInteractionOpen.playerCloseEnough)
        {
            OpenBathroomDoor();
        }

        // Interact with the toilet lid
        if (toiletClosed.playerCloseEnough || toiletOpen.playerCloseEnough)
        {
            OpenToilet();
        }

        // Interact with the Map Selection Display
        if(mapSelectionDistanceCheck.playerCloseEnough)
        {
            mapDisplayToggle.SetActive(true);
            mapStartText.SetActive(true);
        }
        
        // Interact with the Gender Selection Display
        if(genderSelectionDistanceCheck.playerCloseEnough)
        {
            genderDisplayToggle.SetActive(true);
            genderSelectText.SetActive(true);
            genderDisplayPrompt.SetActive(false);
        }

        // Interact with the Accessory Selection Display
        if(accSelectionDistanceCheck.playerCloseEnough)
        {
            accDisplayToggle.SetActive(true);
            accSelectText.SetActive(true);
            accDPadSelect.SetActive(true);
            accDisplayPrompt.SetActive(false);
        }
    }

    private void HandleDPadPress(int direction)
    {
        // Map Interaction
        if (mapDisplayToggle.activeSelf)
        {
            if (mapCurrentlySelected == 1)
            {
                if (direction == 1 || direction == 2)
                {
                    SetColors(0, 0, 1, 0);
                    mapCurrentlySelected = 3;
                }
                else if (direction == 3 || direction == 4)
                {
                    SetColors(0, 1, 0, 0);
                    mapCurrentlySelected = 2;
                }
            }
            else if (mapCurrentlySelected == 2)
            {
                if (direction == 1 || direction == 2)
                {
                    SetColors(0, 0, 0, 1);
                    mapCurrentlySelected = 4;
                }
                else if (direction == 3 || direction == 4)
                {
                    SetColors(1, 0, 0, 0);
                    mapCurrentlySelected = 1;
                }
            }
            else if (mapCurrentlySelected == 3)
            {
                if (direction == 1 || direction == 2)
                {
                    SetColors(1, 0, 0, 0);
                    mapCurrentlySelected = 1;
                }
                else if (direction == 3 || direction == 4)
                {
                    SetColors(0, 0, 0, 1);
                    mapCurrentlySelected = 4;
                }
            }
            else if (mapCurrentlySelected == 4)
            {
                if (direction == 1 || direction == 2)
                {
                    SetColors(0, 1, 0, 0);
                    mapCurrentlySelected = 2;
                }
                else if (direction == 3 || direction == 4)
                {
                    SetColors(0, 0, 1, 0);
                    mapCurrentlySelected = 3;
                }
            }
        }
        else if(genderDisplayToggle.activeSelf) {
            if(genderCurrentlySelected == 1)
            {
                if(direction == 3 || direction == 4)
                {
                    maleButton.GetComponent<Image>().color = Color.white;
                    femaleButton.GetComponent<Image>().color = Color.red;
                    genderCurrentlySelected = 2;
                }
            }
            else
            {
                if (direction == 3 || direction == 4)
                {
                    maleButton.GetComponent<Image>().color = Color.red;
                    femaleButton.GetComponent<Image>().color = Color.white;
                    genderCurrentlySelected = 1;
                }
            }
        }
        else if(accDisplayToggle.activeSelf)
        {
            if(direction == 4)
            {
                if(accCursonLocation == 14)
                {
                    accCursonLocation = 1;
                } 
                else
                {
                    accCursonLocation++;
                }
            }
            else if (direction == 3)
            {
                if(accCursonLocation == 1)
                {
                    accCursonLocation = 14;
                }
                else
                {
                    accCursonLocation--;
                }
            }
            playerAccessoryManagement.HandleCursor(accCursonLocation);
        }
    }

    private void HandleMenuSelection()
    {
        if(currentActiveMap == 0)
        {
            if (mapDisplayToggle.activeSelf)
            {
                LaunchMap();
            }
            else if (genderDisplayToggle.activeSelf)
            {
                if (genderCurrentlySelected == 1)
                {
                    maleModel.SetActive(true);
                    femaleModel.SetActive(false);
                }
                else
                {
                    maleModel.SetActive(false);
                    femaleModel.SetActive(true);
                }
            }
            else if (accDisplayToggle.activeSelf)
            {
                playerAccessoryManagement.HandleButtonColorsAndObjects(accCursonLocation);
            }
        }
    }

    private void SetColors(int but1, int but2, int but3, int but4)
    {
        if (but1 == 1)
        {
            urbanButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            urbanButton.GetComponent<Image>().color = Color.white;
        }
        if (but2 == 1)
        {
            suburbButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            suburbButton.GetComponent<Image>().color = Color.white;
        }
        if (but3 == 1)
        {
            graveyardButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            graveyardButton.GetComponent<Image>().color = Color.white;
        }
        if (but4 == 1)
        {
            tbdButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            tbdButton.GetComponent<Image>().color = Color.white;
        }
    }

    private void OpenBathroomDoor()
    {
        if (bathroomDoorClosed.activeSelf)
        {
            bathroomDoorClosed.SetActive(false);
            bathroomDoorOpen.SetActive(true);
        }
        else
        {
            bathroomDoorClosed.SetActive(true);
            bathroomDoorOpen.SetActive(false);
        }
    }

    private void OpenToilet()
    {
        if (toiletCoverClosed.activeSelf)
        {
            toiletCoverClosed.SetActive(false);
            toiletCoverOpen.SetActive(true);
        }
        else
        {
            toiletCoverClosed.SetActive(true);
            toiletCoverOpen.SetActive(false);
        }
    }

    private void LaunchMap()
    {
        if (mapDisplayToggle.activeSelf && mapCurrentlySelected == 1)
        {
            currentActiveMap = 1;
            HandleDisplayEnable(mainMonsterManager.urbanMonsterSpawn);
            DisableHomebaseItems();
            player.transform.position = urbanSpawnPoint;
        }
        else if(mapDisplayToggle.activeSelf && mapCurrentlySelected == 2)
        {
            currentActiveMap = 2;
            HandleDisplayEnable(mainMonsterManager.suburbMonsterSpawn);
            DisableHomebaseItems();
            player.transform.position = suburbSpawnPoint;
        }
        else if(mapDisplayToggle.activeSelf && mapCurrentlySelected == 3)
        {
            currentActiveMap = 3;
            HandleDisplayEnable(mainMonsterManager.map3MonsterSpawn);
            DisableHomebaseItems();
            player.transform.position = map3SpawnPoint;
        }
        else if(mapDisplayToggle.activeSelf && mapCurrentlySelected == 4)
        {
            currentActiveMap = 4;
            HandleDisplayEnable(mainMonsterManager.map4MonsterSpawn);
            DisableHomebaseItems();
            player.transform.position = map4SpawnPoint;
        }
    }

    private void HandleDisplayEnable(int monster)
    {
        if(monster == 1)
        {
            clueDisplayManager.OnSceneLoad(monster);
        }
        else if(monster == 2)
        {
            clueDisplayManager.OnSceneLoad(monster);
        }
        else if(monster == 3)
        {
            clueDisplayManager.OnSceneLoad(monster);
        }
        else if(monster == 4)
        {
            clueDisplayManager.OnSceneLoad(monster);
        }
    }

    private void DisableHomebaseItems()
    {
        controlsPanel.SetActive(false);
        genderPanel.SetActive(false);
        accessoriesPanel.SetActive(false);
        mapPanel.SetActive(false);
    }

    public void EnableHomebaseItems()
    {
        monstersDefeated++;
        combatScreenDisable.SetActive(false);
        controlsPanel.SetActive(true);
        genderPanel.SetActive(true);
        accessoriesPanel.SetActive(true);
        mapPanel.SetActive(true);
    }
}
