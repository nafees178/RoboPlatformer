using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Values")]
    [Tooltip("Change the player's movement speed")]
    [SerializeField] private float playerMoveSpeed = 5f;
    [Header("Change the player's sprint speed with a multiplier")]
    [SerializeField] private float playerSprintMultiplier = 1.5f;
    [Tooltip("Change the player's maximum jump Height")]
    [SerializeField] private float playerMaxJumpHeight = 2f;
    [Tooltip("Set the player's 'Sneak' speed (Slower)")]
    [SerializeField] private float playerSneakSpeed = 2.5f;
    [Tooltip("Set the maximum number of jumps allowed")]
    [SerializeField] private int maxJumps = 2;

    [Space]

    [Header("Controller Properties")]
    [SerializeField] private float inAirGravity = -9.8f;
    [SerializeField] private float onLandGravity = -0.5f;

    [Space]

    [Header("Player Input Controls")]
    [Tooltip("What input to move the player side to side?")]
    [SerializeField] private InputAction playerMovementIA;
    [Tooltip("What input to make the player sprint?")]
    [SerializeField] private InputAction playerSprintIA;
    [Tooltip("What input to make the player jump?")]
    [SerializeField] private InputAction playerJumpIA;
    [Tooltip("What input to make the player sneak?")]
    [SerializeField] private InputAction playerSneakIA;

    [Space]
    [Header("Refrences")]
    [SerializeField] Animator animator;
    [SerializeField] GameObject playerModel;
    CharacterController characterController;
    SideScrollerCamera cameraScript;
    Vector3 velocity;

    [Space]
    [Header("Debugging Variables")]
    [SerializeField] bool wasGrounded = true; // Tracks previous ground state
    [SerializeField] bool isGrounded; //for debugging;
    [SerializeField] bool isSprinting;
    [SerializeField] bool isSneaking;
    [SerializeField] int jumpCount = 0;

    private void Awake()
    {
        Debug.Log("Are all the InputAction's enabled?"); // Debug message to check if input actions are enabled
        characterController = GetComponent<CharacterController>();
        cameraScript = Camera.main.GetComponent<SideScrollerCamera>(); //Gets the refrence from main camera
        cameraScript.player = gameObject.transform;
    }

    private void OnEnable()
    {
        playerJumpIA.Enable();
        playerMovementIA.Enable();
        playerSneakIA.Enable();
        playerSprintIA.Enable();
        playerJumpIA.performed += OnJumpInput;
    }

    private void OnDisable()
    {
        playerJumpIA.performed -= OnJumpInput;
    }

    private void OnJumpInput(InputAction.CallbackContext ctx)
    {
        Jump();
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
        MovePlayer();
        ApplyGravity();
    }

    private void MovePlayer()
    {
        float moveX = playerMovementIA.ReadValue<float>();
        float moveSpeed = playerMoveSpeed;

        // Make the player sprint or sneak based on input
        isSprinting = playerSprintIA.IsPressed();
        isSneaking = playerSneakIA.IsPressed();

        if (isSprinting && !isSneaking)
        {
            moveSpeed *= playerSprintMultiplier;
            Debug.Log("Sprinting...");
        }
        else if (isSneaking)
        {
            moveSpeed = playerSneakSpeed;
            Debug.Log("Sneaking...");
        }

        //Rotate Player Model based on where it is moving
        if (moveX > 0)
            playerModel.transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (moveX < 0)
            playerModel.transform.rotation = Quaternion.Euler(0, -90, 0);

        Vector3 move = new Vector3(moveX, 0, 0).normalized * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(move.x)); //Set the speed parameter in the animator
        characterController.Move(move * Time.deltaTime);
    }

    private void Jump()
    {
        if (jumpCount == 0 && isGrounded || jumpCount != 0) //Doens't allow player to start jumping when player is not in ground
        {
            if (jumpCount < maxJumps) //Allows for multiple Jump if needed
            {
                velocity.y = Mathf.Sqrt(playerMaxJumpHeight * -2f * inAirGravity); // Applies jump force
                jumpCount++;
                Debug.Log("Jumping...");
                if (isGrounded) //Plays Jump Animation When Player is Grounded
                {
                    animator.SetBool("Jump", true);
                }
                else //Plays In Air Jump Animation
                {
                    animator.SetTrigger("JumpInAir");
                }

            }
            characterController.Move(velocity * Time.deltaTime);
        }

    }

    private void ApplyGravity()
    {
        if (isGrounded)
        {
            if (!wasGrounded) // Just landed
            {
                velocity.y = onLandGravity;
                jumpCount = 0;

                if (cameraScript != null)
                    cameraScript.OnLand(); // Trigger camera shake once

                //Reset Animator Parameters 
                animator.SetBool("Jump", false);
                animator.ResetTrigger("JumpInAir");

                wasGrounded = true;
            }
        }
        else
        {
            wasGrounded = false; // Mark as in-air

            // Apply gravity but clamp velocity to prevent excessive falling speed
            velocity.y += inAirGravity * Time.deltaTime;
            velocity.y = Mathf.Max(velocity.y, -20f); // Limit max fall speed
        }

        characterController.Move(velocity * Time.deltaTime);
    }


}
