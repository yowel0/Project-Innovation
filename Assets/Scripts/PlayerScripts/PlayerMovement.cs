using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
    [Range(0f, 100f)]
    [SerializeField] float disableSprintStaminaThreshold;
    [Range(0f, 100f)]
    [SerializeField] float enableSprintStaminaThreshold;
    [SerializeField] TextMeshProUGUI staminaText;

    [Header("Sounds")]
    [SerializeField] AudioSource footStepSource;
    [SerializeField] AudioSource staminaSoundSource;
    [SerializeField] AudioClip walkStepSound;
    [SerializeField] AudioClip sprintStepSound;
    [SerializeField] AudioClip regainStaminaSound;
    [SerializeField] AudioClip playerDeathSound;

    [Header("Orientation")]
    public Transform orientation;  // Transform used for orientation (typically the player's body)

    float horizontalInput;  // Horizontal input (-1, 0, 1)
    float verticalInput;    // Vertical input (-1, 0, 1)

    Vector3 moveDirection;  // Calculated movement direction

    Rigidbody rb;  // Reference to the Rigidbody component

    Vector3 respawnPos;
    float storedMoveSpeed;
    bool isWalking;
    bool canSprint = true;
    bool isDead;

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
        respawnPos = rb.position;
        rb.drag = groundDrag;

        DeathManager.OnDeath += PlayerDead;
        DeathManager.OnRespawn += Respawn;

        if (staminaSoundSource == null && footStepSource == null)
        {
            Debug.Log("Please set the sound sources correctly");
            AudioSource[] sources = GetComponents<AudioSource>();
            if (sources.Length >= 2)
            {
                footStepSource = sources[0];
                staminaSoundSource = sources[1];
            }
        }
    }

    private void Update()
    {
        if (isDead) return;
        MyInput();      // Read player input
        SpeedControl(); // Control player movement speed
        
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        MovePlayer();  // Move the player based on calculated input
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");  // Get horizontal input (left/right keys or A/D keys)
        verticalInput = Input.GetAxisRaw("Vertical");      // Get vertical input (up/down keys or W/S keys)

        SprintFunctions();
    }

    private void SprintFunctions()
    {
        moveSpeed = storedMoveSpeed;
        if (staminaText != null) 
        { 
            if (currentStamina <= disableSprintStaminaThreshold) staminaText.color = Color.red;
            else staminaText.color = Color.white;            
        }

        if (canSprint)
        {
            if (Input.GetKey(sprintKey))
            {
                // Succesfully sprinting
                SetFootstepSound(sprintStepSound);
                currentStamina -= staminaConsumption;
                moveSpeed = sprintSpeed;
                if (currentStamina <= 0) DisableSprint();
            }
            else
            {
                if (currentStamina <= disableSprintStaminaThreshold) DisableSprint();
                RegainStamina();
            }
        }
        else
        {
            RegainStamina();
            if (staminaText != null) staminaText.color = Color.red;
        }

        if (staminaText != null)staminaText.text = "Stamina: " + (int)currentStamina;

    }

    void DisableSprint()
    {
        canSprint = false;
        if (staminaSoundSource != null && regainStaminaSound != null) staminaSoundSource.PlayOneShot(regainStaminaSound);
    }
    void RegainStamina()
    {
        SetFootstepSound(walkStepSound);
        currentStamina = Mathf.Min(currentStamina + staminaRegeneration, maxStamina);
        if (!canSprint && currentStamina >= enableSprintStaminaThreshold) canSprint = true;
        if (currentStamina >= maxStamina)
        {
            if (staminaText != null) staminaText.color = Color.green;
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

        if (isWalking && SoundLoudnessManager.GetManager() != null)
        {
            SoundLoudnessManager.GetManager().CheckLoudness(footStepSource.clip.name);
        }

        // Calculate the new position based on the move direction and speed
        Vector3 newPosition = rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;

        // Move the player by setting the new position
        rb.MovePosition(newPosition);

        if (rb.position.y < -10) Respawn();
    }

    void SetFootstepSound(AudioClip sound)
    {
        if (footStepSource.clip == null || footStepSource.clip.name != sound.name)
        {
            isWalking = false;
            footStepSource.clip = sound;
        }
    }
    void SetStaminaSound(AudioClip sound)
    {
        if (staminaSoundSource.clip == null || staminaSoundSource.clip.name != sound.name)
        {
            staminaSoundSource.clip = sound;
        }
    }
    void PlayWalkSFX()
    {
        if (footStepSource != null)
        {
            if (isWalking)
            {
                footStepSource.Play();
                if (SoundLoudnessManager.GetManager() != null)
                {
                    SoundLoudnessManager.GetManager().CheckLoudness(footStepSource.clip.name);
                }
            }
            else
            {
                footStepSource.Stop();
            }
        }
        else
        {
            Debug.Log("PlayerMovement: Cannot play sound. footStepSource = " + (footStepSource != null));
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
        DeathManager.OnDeath -= PlayerDead;
        DeathManager.OnRespawn -= Respawn;
    }
    void PlayerDead()
    {
        isDead = true;
        isWalking = false;
        PlayWalkSFX();
        staminaSoundSource.PlayOneShot(playerDeathSound);
    }

    void Respawn()
    {
        rb.position = respawnPos;
        isDead = false;
    }

    public void SetRespawn()
    {
        SetRespawn(rb.position);
    }
    public void SetRespawn(Vector3 pos)
    {
        respawnPos = pos;
    }
}
