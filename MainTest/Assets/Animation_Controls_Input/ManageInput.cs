using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManageInput : MonoBehaviour
{
    ControllingPlayer playerControls;
    Locomotion playerLocomotion;
    ManageAnimation animatorManager;
    PlayerCombat playerCombat;

    [Header("Clue System")]
    public GameObject clueSystem;
    CluePickUpManager cluePickUpManager;

    [Header("Toggle Flashlight")]
    public GameObject flashLight;
    private bool flashLightOn;

    [Header("Guess Machine")]
    public GameObject urbanGuess;
    public GameObject suburbGuess;
    public GameObject map3Guess;
    public GameObject map4Guess;
    GuessManager urbanGuessManager;
    GuessManager suburbGuessManager;
    GuessManager map3GuessManager;
    GuessManager map4GuessManager;

    [Header("Weapon Selection Racko")]
    // public GameObject weaponObject;
    // ManageWeapons weaponManager;
    public GameObject urbanWeaponObject;
    ManageWeapons urbanWeaponsManager;
    public GameObject suburbWeaponObject;
    ManageWeapons suburbWeaponsManager;
    public GameObject map3WeaponObject;
    ManageWeapons map3WeaponsManager;
    public GameObject map4WeaponObject;
    ManageWeapons map4WeaponsManager;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    PauseMenuManagement pauseManager;
    private int pauseMenuCursorLocation;

    [Header("Home Base Interaction")]
    public GameObject homeBase;
    HomeBaseInteractions homeBaseInteractions;

    // Movement
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    // Sprint Function
    public bool b_input;
    public bool jump_input;

    // Stealth
    public bool currentlySneaking = false;

    // Object Interaction
    public bool itemPickedUp;

    public float cameraInputX;
    public float cameraInputY;

    private void Awake()
    {
        playerLocomotion = GetComponent<Locomotion>();
        animatorManager = GetComponent<ManageAnimation>();
        playerCombat = GetComponent<PlayerCombat>();

        cluePickUpManager = clueSystem.GetComponent<CluePickUpManager>();

        urbanGuessManager = urbanGuess.GetComponent<GuessManager>();
        suburbGuessManager = suburbGuess.GetComponent<GuessManager>();
        map3GuessManager = map3Guess.GetComponent<GuessManager>();
        map4GuessManager = map4Guess.GetComponent<GuessManager>();

        // weaponManager = weaponObject.GetComponent<ManageWeapons>();
        urbanWeaponsManager = urbanWeaponObject.GetComponent<ManageWeapons>();
        suburbWeaponsManager = suburbWeaponObject.GetComponent<ManageWeapons>();
        map3WeaponsManager = map3WeaponObject.GetComponent<ManageWeapons>();
        map4WeaponsManager = map4WeaponObject.GetComponent<ManageWeapons>();

        flashLightOn = false;
        pauseMenu.SetActive(false);
        pauseManager = pauseMenu.GetComponent<PauseMenuManagement>();
        pauseMenuCursorLocation = 1;

        homeBaseInteractions = homeBase.GetComponent<HomeBaseInteractions>();
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new ControllingPlayer();
            
            // Movement
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            // Sprinting
            playerControls.PlayerAction.Sprint.performed += i => b_input = true;
            playerControls.PlayerAction.Sprint.canceled += i => b_input = false;

            // Jumping
            // Remove comment to re-enable jumping feature
            //playerControls.PlayerAction.Jump.performed += i => jump_input = true;

            // Sneak - Crouch
            playerControls.PlayerAction.Sneak.performed += i => FlipSneak();

            // Melee Attack
            playerControls.PlayerAction.Attack.started += DoAttack;

            // Object Interact
            playerControls.PlayerAction.ObjectInteract.performed += i => HandleObjectInteraction();

            // Menu Interaction
            playerControls.MenuActions.NavUp.performed += i => HandleDPadPress(1);
            playerControls.MenuActions.NavRight.performed += i => HandleDPadPress(2);
            playerControls.MenuActions.NavDown.performed += i => HandleDPadPress(3);
            playerControls.MenuActions.NavLeft.performed += i => HandleDPadPress(4);

            // Toggle Flashlight
            playerControls.PlayerAction.FlashlightToggle.performed += i => HandleFlashLightStatus();

            // Toggle Pause Menu
            playerControls.MenuActions.Pause.performed += i => OpenPauseMenu();
            playerControls.MenuActions.Select.performed += i => PauseMenuSelection();
        }

        playerControls.Enable();
    }

   

    private void OnDisable()
    {
        playerControls.PlayerAction.Attack.started -= DoAttack;
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleJumpingInput();
        HandleSneak();

        // Handle Action Input
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintInput()
    {
        if(b_input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpingInput()
    {
        if(jump_input)
        {
            jump_input = false;
            playerLocomotion.HandleJumping();
        }
    }

    private bool FlipSneak()
    {
        if(currentlySneaking)
        {
            currentlySneaking = false;
        } else
        {
            currentlySneaking = true;
        }
        return currentlySneaking;
    }

    private void HandleSneak()
    {
        if(currentlySneaking)
        {
            // Debug.Log("ENTER stealth");
            playerLocomotion.isSneak = true;
            
        } 
        else
        {
            // Debug.Log("LEAVE stealth");
            playerLocomotion.isSneak = false;
        }
        playerLocomotion.HandleSneaking();
    }

    // Melee Attack
    private void DoAttack(InputAction.CallbackContext obj)
    {
        int weaponAttack = 0;
        #region Get Equipped Weapon
        if (homeBaseInteractions.currentActiveMap == 1)
        {
            weaponAttack = urbanWeaponsManager.equippedWeapon;
        }
        else if(homeBaseInteractions.currentActiveMap == 2)
        {
            weaponAttack = suburbWeaponsManager.equippedWeapon;
        }
        else if(homeBaseInteractions.currentActiveMap == 3)
        {
            weaponAttack = map3WeaponsManager.equippedWeapon;
        }
        else if(homeBaseInteractions.currentActiveMap == 4)
        {
            weaponAttack = map4WeaponsManager.equippedWeapon;
        }
        #endregion

        #region Weapon Attack Animation
        if (weaponAttack == 1)
        {
            animatorManager.animator.SetTrigger("knifeAttack");
        }
        else if(weaponAttack == 2)
        {
            animatorManager.animator.SetTrigger("butcherAttack");
        }
        else if(weaponAttack == 3)
        {
            animatorManager.animator.SetTrigger("axeAttack");
        }
        else if(weaponAttack == 4)
        {
            animatorManager.animator.SetTrigger("spearAttack");
        }
        else
        {
            animatorManager.animator.SetTrigger("punchAttack");
        }
        #endregion

        playerCombat.StartAttack(weaponAttack);
    }

    // Object Interact
    private void HandleObjectInteraction()
    {
        cluePickUpManager.HandleClueInteraction();

        urbanGuessManager.HandleGuessInteraction();
        suburbGuessManager.HandleGuessInteraction();
        map3GuessManager.HandleGuessInteraction();
        map4GuessManager.HandleGuessInteraction();

        #region Weapon Rack Interaction
        if (homeBaseInteractions.currentActiveMap == 1)
        {
            urbanWeaponsManager.HandleWeaponRack(0);
        }
        else if (homeBaseInteractions.currentActiveMap == 2)
        {
            suburbWeaponsManager.HandleWeaponRack(0);
        }
        else if (homeBaseInteractions.currentActiveMap == 3)
        {
            map3WeaponsManager.HandleWeaponRack(0);
        }
        else if (homeBaseInteractions.currentActiveMap == 4)
        {
            map4WeaponsManager.HandleWeaponRack(0);
        }
        #endregion
    }

    private void HandleDPadPress(int padDirection)
    {
        if(pauseMenu.activeSelf)
        {
            if(padDirection == 1)
            {
                if(pauseMenuCursorLocation == 1)
                {
                    pauseMenuCursorLocation = 3;
                }
                else
                {
                    pauseMenuCursorLocation--;
                }
            }
            else if(padDirection == 3)
            {
                if(pauseMenuCursorLocation == 3)
                {
                    pauseMenuCursorLocation = 1;
                }
                else
                {
                    pauseMenuCursorLocation++;
                }
            }
            pauseManager.HandleDirectionInput(padDirection);
        }

        #region Monster Guess Submission
        if(homeBaseInteractions.currentActiveMap == 1)
        {
            if (urbanWeaponsManager.weaponScreenOpen)
            {
                urbanWeaponsManager.HandleWeaponRack(padDirection);
            }
            else
            {
                urbanGuessManager.PlayerGuessSubmission(padDirection);
            }  
        }
        else if(homeBaseInteractions.currentActiveMap == 2)
        {
            if(suburbWeaponsManager.weaponScreenOpen)
            {
                suburbWeaponsManager.HandleWeaponRack(padDirection);
            }
            else
            {
                suburbGuessManager.PlayerGuessSubmission(padDirection);
            }
        }
        else if(homeBaseInteractions.currentActiveMap == 3)
        {
            if(map3WeaponsManager.weaponScreenOpen)
            {
                map3WeaponsManager.HandleWeaponRack(padDirection);
            } else
            {
                map3GuessManager.PlayerGuessSubmission(padDirection);
            }  
        }
        else if(homeBaseInteractions.currentActiveMap == 4)
        {
            if(map4WeaponsManager.weaponScreenOpen)
            {
                map4WeaponsManager.HandleWeaponRack(padDirection);
            }
            else
            {
                map4GuessManager.PlayerGuessSubmission(padDirection);
            } 
        }
        #endregion

        
    }

    private void HandleFlashLightStatus()
    {
        if(flashLightOn)
        {
            flashLightOn = false;
            flashLight.SetActive(false);
        } 
        else
        {
            flashLightOn = true;
            flashLight.SetActive(true);
        }
    }

    private void OpenPauseMenu()
    {
        if(pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    private void PauseMenuSelection()
    {
        if(pauseMenuCursorLocation == 1)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseManager.PauseMenuSeleciton();
        }
    }
}
