using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManageInput : MonoBehaviour
{
    ControllingPlayer playerControls;
    Locomotion playerLocomotion;
    ManageAnimation animatorManager;

    [Header("Clue System")]
    public GameObject clueObject;
    ClueManager clueManager;

    [Header("Toggle Flashlight")]
    public GameObject flashLight;
    private bool flashLightOn;

    [Header("Guess Machine")]
    public GameObject guessObject;
    GuessMachine guessManager;

    [Header("Weapon Selection Rack")]
    public GameObject weaponObject;
    ManageWeapons weaponManager;

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
        // clueObject.GetComponent<ClueManager>();
        clueManager = clueObject.GetComponent<ClueManager>();
        guessManager = guessObject.GetComponent<GuessMachine>();
        weaponManager = weaponObject.GetComponent<ManageWeapons>();
        flashLightOn = true;
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
            playerControls.MenuActions.NavDown.performed += i => HandleDPadPress(2);
            playerControls.MenuActions.NavLeft.performed += i => HandleDPadPress(3);
            playerControls.MenuActions.NavRight.performed += i => HandleDPadPress(4);

            // Toggle Flashlight
            playerControls.PlayerAction.FlashlightToggle.performed += i => HandleFlashLightStatus();
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
        int weaponAttack = weaponManager.equippedWeapon;
        if(weaponAttack == 0)
        {
            animatorManager.animator.SetTrigger("punchAttack");
        }
        else if(weaponAttack == 2)
        {
            animatorManager.animator.SetTrigger("knifeAttack");
        }
        else
        {
            animatorManager.animator.SetTrigger("attack");
        }

        /*
        if(weaponAttack == 0)
        {
            animatorManager.animator.SetTrigger("punchAttack");
        }
        else if(weaponAttack == 1)
        {
            animatorManager.animator.SetTrigger("baseBallBatAttack");
        }
        else if(weaponAttack == 2)
        {
            animatorManager.animator.SetTrigger("knifeAttack");
        }
        else if(weaponAttack == 3)
        {
            animatorManager.animator.SetTrigger("butcherAttack");
        }
        else if(weaponAttack == 4)
        {
            animatorManager.animator.SetTrigger("hatchetAttack");
        }
        else
        {
            animatorManager.animator.SetTrigger("attack");
        }
        */
    }

    // Object Interact
    private void HandleObjectInteraction()
    {
        clueManager.HandleCluePick();
        guessManager.HandleGuessMachine();
        weaponManager.HandleWeaponRack(0);
    }

    private void HandleDPadPress(int padDirection)
    {
        if(guessManager.guessScreenOpen)
        {
            guessManager.PlayerGuessSubmission(padDirection);
        }
        if(weaponManager.weaponScreenOpen)
        {
            weaponManager.HandleWeaponRack(padDirection);
        }
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
}
