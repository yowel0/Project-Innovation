using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static event Action OnSprint;
    [Header("Movement")]
    [SerializeField] float moveSpeed;  // Speed of movement
    [SerializeField] float groundDrag; // Drag applied when grounded

    [Header("Sprinting")]
    [SerializeField] float sprintSpeed;// Sprinting speed
    [SerializeField] KeyCode sprintKey;
    [SerializeField] float currentStamina;
    [SerializeField] float maxStamina;
    [SerializeField] float staminaConsumption;
    [SerializeField] float staminaRegeneration;


    [Header("Ground Check")]
    public float playerHeight;  // Height of the player collider
    public LayerMask whatIsGround;  // Layer mask to define what is considered ground
    bool grounded;  // Flag indicating if the player is grounded

    [Header("Orientation")]
    public Transform orientation;  // Transform used for orientation (typically the player's body)

    float horizontalInput;  // Horizontal input (-1, 0, 1)
    float verticalInput;    // Vertical input (-1, 0, 1)

    Vector3 moveDirection;  // Calculated movement direction

    Rigidbody rb;  // Reference to the Rigidbody component

    Vector3 startPos;
    float storedMoveSpeed;
    bool isWalking;

    AudioSource audioPlayer;

    public static PlayerMovement GetPlayer()
    {
        return playerSingleton;
    }
    static PlayerMovement playerSingleton = null;

    private void Awake()
    {
        if (playerSingleton == null)
        {
            playerSingleton = this;
        }
        else
        {
            throw new System.Exception("There can only be one PlayerMovement in a scene!");
        }
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get the Rigidbody component attached to this GameObject
        rb.freezeRotation = true;  // Freeze rotation to prevent physics affecting the orientation
        storedMoveSpeed = moveSpeed;
        audioPlayer = GetComponent<AudioSource>();
        startPos = rb.position;
    }

    private void Update()
    {
        // Ground Check using a Raycast
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * (playerHeight * 0.5f + 0.02f), Color.red);

        MyInput();      // Read player input
        SpeedControl(); // Control player movement speed

        // Handle Drag based on grounded state
        if (grounded)
        {
            rb.drag = groundDrag;  // Apply ground drag if grounded
        }
        else
        {
            rb.drag = 0;  // No drag if not grounded
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();  // Move the player based on calculated input
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");  // Get horizontal input (left/right keys or A/D keys)
        verticalInput = Input.GetAxisRaw("Vertical");      // Get vertical input (up/down keys or W/S keys)

        moveSpeed = storedMoveSpeed;

        // Sprinting
        if (Input.GetKey(sprintKey))
        {
            if (currentStamina - staminaConsumption >= 0)
            {
                moveSpeed = sprintSpeed;
                currentStamina -= staminaConsumption;
            }
            OnSprint?.Invoke();
        }
        else
        {
            currentStamina = Mathf.Min(currentStamina + staminaRegeneration, maxStamina);
            if (currentStamina < maxStamina)
            {
                // Add panting sound?
            }
        }

    }

    private void MovePlayer()
    {
        // Calculate movement direction based on orientation and input
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Movement sounds
        if (moveDirection.normalized.magnitude > 0 && !isWalking)
        {
            isWalking = true;
            PlayWalkSFX();
        }
        else if (moveDirection.normalized.magnitude < 0.01f && isWalking)
        {
            isWalking = false;
            PlayWalkSFX();  // Name is misleading
        }

        // Calculate the new position based on the move direction and speed
        Vector3 newPosition = rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;

        // Move the player by setting the new position
        rb.MovePosition(newPosition);

        if (rb.position.y < -10) rb.position = startPos;
    }

    void PlayWalkSFX()
    {
        if (audioPlayer != null)
        {
            if (isWalking)
            {
                //Debug.Log("started walk sfx");
                audioPlayer.Play();
            }
            else
            {
                //Debug.Log("stopped walk sfx");
                audioPlayer.Stop();
            }
        }
        else
        {
            Debug.Log("Interactable: Cannot play sound. audioPlayer = " + (audioPlayer != null));
        }
    }



    public float GetMovementSpeed()
    {
        return moveDirection.magnitude * moveSpeed; // Return calculated movement speed
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;  // Return current velocity of the Rigidbody
    }

    public bool IsGrounded()
    {
        return grounded;  // Return whether the player is grounded
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);  // Remove vertical component from velocity

        // Limiting velocity to moveSpeed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;  // Limit velocity magnitude
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);  // Apply limited velocity
        }
    }
    /*
    public void FreezeMovement()
    {
        if (moveSpeed > .5f)
        {
            storedMoveSpeed = moveSpeed;
            moveSpeed = 0f;
        }
    }

    public void RestoreMoveSpeed()
    {
        moveSpeed = storedMoveSpeed;
    }*/

    private void OnDestroy()
    {
        playerSingleton = null;
    }
}
