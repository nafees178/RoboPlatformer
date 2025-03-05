using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Value's")]
    [Tooltip("Change the player's movement speed")]
    [SerializeField] private float playerMoveSpeed = 5f;
    [Header("Change the player's sprint speed with a multiplier")]
    [SerializeField] private float playerSprintMultiplier = 1.5f;
    [Tooltip("Change the player's jump force")]
    [SerializeField] private float playerJumpForce = 2f;
    [Tooltip("Set the player's 'Sneak' speed (Slower)")]
    [SerializeField] private float playerSneakSpeed = 2.5f;

    [Space]

    [Header("Player Input Control's")]
    [Tooltip("What input to move the player side to side?")]
    [SerializeField] private InputAction playerMovementIA;
    [Tooltip("What input to make the player sprint?")]
    [SerializeField] private InputAction playerSprintIA;
    [Tooltip("What input to make the player jump?")]
    [SerializeField] private InputAction playerJumpIA;
    [Tooltip("What input to make the player sneak?")]
    [SerializeField] private InputAction playerSneakIA;

    // Not Visibile in inspector below:


    // Bools

    bool isSprinting;
    bool isGrounded;
    bool isSneaking;

    // Component's
    CharacterController characterController;

    private void Awake()
    {
        Debug.Log("Are all the InputAction's enabled?"); // I used to forget to enable so i do this each time incase lol

        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveX = playerMovementIA.ReadValue<float>();
        float moveSprint = playerMoveSpeed * playerSprintMultiplier;

        Vector3 movePlayer = new Vector3(moveX, 0, 0).normalized;

        // Make the player sprint (It Works not sure if its the correct way, just how i tought myself lol)
        isSprinting = playerSprintIA.IsPressed();

        if (!isSprinting)
        {
            characterController.Move(movePlayer * playerMoveSpeed * Time.deltaTime);
        }

        if (isSprinting)
        {
            characterController.Move(movePlayer * moveSprint * Time.deltaTime);
            Debug.Log("Sprinting = " + isSprinting);
        }

    }

    private void OnEnable()
    {
        playerJumpIA.Enable();
        playerMovementIA.Enable();
        playerSneakIA.Enable();
        playerSprintIA.Enable();
    }

}
