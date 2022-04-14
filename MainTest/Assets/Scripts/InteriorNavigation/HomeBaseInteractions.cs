using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeBaseInteractions : MonoBehaviour
{
    // Gets the controls input
    ControllingPlayer playerControls;

    [Header("Player")]
    public GameObject player;

    [Header("Bathroom Objects")]
    public GameObject bathroomDoorClosed;
    public GameObject bathroomDoorOpen;
    public GameObject toiletCoverClosed;
    public GameObject toiletCoverOpen;

    [Header("Map Selection Display")]
    public GameObject mapDetectionCube;
    public GameObject mapDisplayToggle;
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

    [Header("Map Spawn Points")]
    public GameObject urbanSpawn;
    private Vector3 urbanSpawnPoint;

    [Header("UI Elements to Disable in Homebase")]
    public GameObject clueScreenDisable;

    // Player Bathroom Interaction Items
    PlayerInteraction doorInteractionClosed;
    PlayerInteraction doorInteractionOpen;
    PlayerInteraction toiletClosed;
    PlayerInteraction toiletOpen;

    // Player Map Interactive Items
    PlayerInteraction mapSelectionDistanceCheck;
    PlayerInteraction genderSelectionDistanceCheck;

    // Distance to player
    public float distanceToTarget;

    // Map Starting Selections
    private int mapCurrentlySelected;
    private int genderCurrentlySelected;

    private void Start()
    {
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

        #region Disable Game UI Items
        clueScreenDisable.SetActive(false);
        #endregion

        #region
        urbanSpawnPoint = urbanSpawn.transform.position;
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
    }

    private void HandleMenuSelection()
    {
        if (mapDisplayToggle.activeSelf)
        {
            if(mapCurrentlySelected == 1)
            {
                Debug.Log("Player Transported to: " + urbanSpawnPoint);
                clueScreenDisable.SetActive(true);
                player.transform.position = urbanSpawnPoint;
            }
        }
        else if(genderDisplayToggle.activeSelf)
        {
            if(genderCurrentlySelected == 1)
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
        if(mapDisplayToggle.activeSelf && mapCurrentlySelected == 1)
        {
            Debug.Log("Player Transported to: " + urbanSpawnPoint);
            clueScreenDisable.SetActive(true);
            player.transform.position = urbanSpawnPoint;
        }
    }
}
