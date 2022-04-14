using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    ManagePlayer playerManager;
    ManageAnimation animationManager;
    ManageInput inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidBody;
    CapsuleCollider capsuleCollider;

    [Header("Falling")]
    public float inAirTime;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;
    public bool isSneak;

    [Header("Movement Speeds")]
    public float sneakSpeed = 2.5f;
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 9;
    public float rotationSpeed = 15;

    [Header("Jump Speed")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    [Header("Player Stance")]
    public int currentStance;
    // 0 = Sneaking
    // 1 = Walking/running
    // 2 = Sprinting


    private void Awake()
    {
        playerManager = GetComponent<ManagePlayer>();
        animationManager = GetComponent<ManageAnimation>();
        inputManager = GetComponent<ManageInput>();
        playerRigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting)
        {
            return;
        }

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if(isJumping)
        {
            return;
        }

        // Movement Input
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if(isSprinting && !isSneak)
        {
            currentStance = 2;
            moveDirection *= sprintingSpeed;
        }
        else if (!isSprinting && isSneak)
        {
            currentStance = 0;
            moveDirection *= sneakSpeed;
        }
        else
        {
            currentStance = 1;
            // Various Moving speeds
            if (inputManager.moveAmount > 0.5f)
            {
                moveDirection *= runningSpeed;
            }
            else
            {
                moveDirection *= walkingSpeed;
            }
        }

        Vector3 movementVelocity = moveDirection;
        playerRigidBody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if(isJumping)
        {
            return;
        }
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;
        targetPosition = transform.position;

        if(!isGrounded && !isJumping)
        {
            //Debug.Log("YOU FELL");
            if (!playerManager.isInteracting)
            {
                //Debug.Log("animate - - - fall");
                animationManager.PlayTargetAnimation("newFallingIdle", true);
            }

            inAirTime = inAirTime + Time.deltaTime;
            playerRigidBody.AddForce(transform.forward * leapingVelocity);
            playerRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTime);
        }

        if(Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            //Debug.Log("YOU HIT THE GROUND");
            if(!isGrounded && !playerManager.isInteracting)
            {
                //Debug.Log("animate - - - land");
                animationManager.PlayTargetAnimation("newLanding", true);
            }

            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTime = 0;
            isGrounded = true;
        }
        else
        {
            //Debug.Log("STILL FALLING ...... ");
            isGrounded = false;
        }

        if(isGrounded && !isJumping)
        {
            if(playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }

    public void HandleJumping()
    {
        if(isGrounded)
        {
            animationManager.animator.SetBool("isJumping", true);
            animationManager.PlayTargetAnimation("Jumping", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidBody.velocity = playerVelocity;
        }
    }

    // Method to handle sneaking animations
    public void HandleSneaking()
    {
        if(!isSprinting && isGrounded && !isJumping)
        {
            if(isSneak)
            {
                animationManager.animator.SetBool("isSneaking", true);
                capsuleCollider.height = 1.2f;
                capsuleCollider.center = new Vector3(0, 0.9f, 0);
            }
            else
            {
                animationManager.animator.SetBool("isSneaking", false);
                capsuleCollider.height = 1.65f;
                capsuleCollider.center = new Vector3(0, 1.1f, 0);
            }
            
        }
    }
}
