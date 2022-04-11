using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteriorNavigation : MonoBehaviour
{
    ControllingPlayer playerControls;

    [Header("Player")]
    public GameObject player;

    [Header("Bathroom Objects")]
    public GameObject bathroomDoorClosed;
    public GameObject bathroomDoorOpen;
    public GameObject toiletCoverClosed;
    public GameObject toiletCoverOpen;

    [Header("Map Hover Text")]
    public GameObject desk;
    public GameObject hoverText;
    public GameObject selectionScreen;
    public GameObject xToStartText;
    public GameObject clueScreenDisable;

    [Header("Menu Buttons")]
    public Button urbanButton;
    public Button suburbButton;
    public Button graveyardButton;
    public Button tbdButton;

    PlayerInteraction doorInteractionClosed;
    PlayerInteraction doorInteractionOpen;
    PlayerInteraction toiletClosed;
    PlayerInteraction toiletOpen;
    PlayerInteraction deskDistanceCheck;

    private int currentlySelected;

    // Distance to player
    public float distanceToTarget;

    private void Start()
    {
        doorInteractionClosed = bathroomDoorClosed.GetComponent<PlayerInteraction>();
        doorInteractionOpen = bathroomDoorOpen.GetComponent<PlayerInteraction>();
        toiletClosed = toiletCoverClosed.GetComponent<PlayerInteraction>();
        toiletOpen = toiletCoverOpen.GetComponent<PlayerInteraction>();
        deskDistanceCheck = desk.GetComponent<PlayerInteraction>();

        bathroomDoorClosed.SetActive(true);
        bathroomDoorOpen.SetActive(false);
        toiletCoverClosed.SetActive(true);
        toiletCoverOpen.SetActive(false);
        hoverText.SetActive(false);
        selectionScreen.SetActive(false);
        xToStartText.SetActive(false);
        clueScreenDisable.SetActive(false);

        currentlySelected = 1;
        urbanButton.GetComponent<Image>().color = Color.red;
        suburbButton.GetComponent<Image>().color = Color.white;
        graveyardButton.GetComponent<Image>().color = Color.white;
        tbdButton.GetComponent<Image>().color = Color.white;
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new ControllingPlayer();

            playerControls.PlayerAction.ObjectInteract.performed += i => HandleObjectInteraction();

            playerControls.MenuActions.SelectWithY.performed += i => LaunchMap();

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

    private void Update()
    {
        if(deskDistanceCheck.playerCloseEnough)
        {
            hoverText.SetActive(true);
        }
        else
        {
            hoverText.SetActive(false);
            selectionScreen.SetActive(false);
        }
    }

    private void HandleDPadPress(int direction)
    {
        if(selectionScreen.activeSelf)
        {
            if(currentlySelected==1)
            {
                if(direction == 1 || direction == 2)
                {
                    SetColors(0, 0, 1, 0);
                    currentlySelected = 3;
                }
                else if(direction == 3 || direction == 4)
                {
                    SetColors(0, 1, 0, 0);
                    currentlySelected = 2;
                }
            }
            else if (currentlySelected == 2)
            {
                if (direction == 1 || direction == 2)
                {
                    SetColors(0, 0, 0, 1);
                    currentlySelected = 4;
                }
                else if (direction == 3 || direction == 4)
                {
                    SetColors(1, 0, 0, 0);
                    currentlySelected = 1;
                }
            }
            else if (currentlySelected == 3)
            {
                if (direction == 1 || direction == 2)
                {
                    SetColors(1, 0, 0, 0);
                    currentlySelected = 1;
                }
                else if (direction == 3 || direction == 4)
                {
                    SetColors(0, 0, 0, 1);
                    currentlySelected = 4;
                }
            }
            else if (currentlySelected == 4)
            {
                if (direction == 1 || direction == 2)
                {
                    SetColors(0, 1, 0, 0);
                    currentlySelected = 2;
                }
                else if (direction == 3 || direction == 4)
                {
                    SetColors(0, 0, 1, 0);
                    currentlySelected = 3;
                }
            }
        }
    }

    private void SetColors(int but1, int but2, int but3, int but4)
    {
        if(but1 == 1)
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

    private void HandleObjectInteraction()
    {
        if(doorInteractionClosed.playerCloseEnough || doorInteractionOpen.playerCloseEnough)
        {
            OpenBathroomDoor();
        }
        if(toiletClosed.playerCloseEnough || toiletOpen.playerCloseEnough)
        {
            OpenToilet();
        }
        if(hoverText.activeSelf)
        {
            selectionScreen.SetActive(true);
            xToStartText.SetActive(true);
        }
    }

    private void OpenBathroomDoor()
    {
        // Debug.Log("X clicked");
        if(bathroomDoorClosed.activeSelf)
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
        if(toiletCoverClosed.activeSelf)
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
        if (selectionScreen.activeSelf && currentlySelected == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
