using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    ControllingPlayer playerControls;

    // Objects for menu items
    [Header("Menu Button Texts")]
    public Text playText;
    public Text loadText;
    public Text optionsText;
    public Text exitText;

    // Keeps track of the currently selected menu item
    private int currentlySelectedItem;

    private bool playActive;
    private bool loadActive;
    private bool optionsActive;
    private bool exitActive;

    // Preset for default red color
    private Color redPreset = new Color(0.5568628f,0f,0f);

    private void Awake()
    {
        playActive = true;
        loadActive = false;
        optionsActive = false;
        exitActive = false;

        // Sets started default items
        currentlySelectedItem = 1;
        playText.color = Color.white;
        loadText.color = redPreset;
        optionsText.color = redPreset;
        exitText.color = redPreset;
    }

    private void Update()
    {
        if(currentlySelectedItem == 1)
        {
            playText.color = Color.white;
            loadText.color = redPreset;
            optionsText.color = redPreset;
            exitText.color = redPreset;
        }
        else if(currentlySelectedItem == 2)
        {
            playText.color = redPreset;
            loadText.color = Color.white;
            optionsText.color = redPreset;
            exitText.color = redPreset;
        }
        else if(currentlySelectedItem == 3)
        {
            playText.color = redPreset;
            loadText.color = redPreset;
            optionsText.color = Color.white;
            exitText.color = redPreset;
        }
        else if(currentlySelectedItem == 4)
        {
            playText.color = redPreset;
            loadText.color = redPreset;
            optionsText.color = redPreset;
            exitText.color = Color.white;
        }
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new ControllingPlayer();

            // Menu Interaction
            playerControls.MenuActions.NavUp.performed += i => HandleDPadPress(1);
            playerControls.MenuActions.NavDown.performed += i => HandleDPadPress(2);
            // playerControls.MenuActions.NavLeft.performed += i => HandleDPadPress(3);
            // playerControls.MenuActions.NavRight.performed += i => HandleDPadPress(4);

            // Menu Selection
            playerControls.MenuActions.Select.performed += i => HandleMenuSelection(currentlySelectedItem);
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleDPadPress(int padDirection)
    {
        if(padDirection == 1)
        {
            if(currentlySelectedItem == 1)
            {
                currentlySelectedItem = 4;
            }
            else
            {
                currentlySelectedItem--;
            }
        }
        else if(padDirection == 2)
        {
            if(currentlySelectedItem == 4)
            {
                currentlySelectedItem = 1;
            }
            else
            {
                currentlySelectedItem++;
            }
        }
    }

    private void HandleMenuSelection(int selectedItem)
    {
        switch(selectedItem) {
            case 1:
                Debug.Log("Play the game");
                playGame();
                break;
            case 2:
                Debug.Log("Load a game");
                break;
            case 3:
                Debug.Log("Options");
                break;
            case 4:
                Debug.Log("Quit the game");
                quitGame();
                break;
        }
    }

    public void playGame()
    {
        //loads scene from queue, work out later
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}
