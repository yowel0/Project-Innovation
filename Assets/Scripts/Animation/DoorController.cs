using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public bool isOpen = false;
    public float raycastRange = 5f;

    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioSource audioPlayer;

    public Camera playerCamera;  // Drag and drop the player camera in the inspector

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange) && hit.transform == transform)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleDoor();
                Debug.Log("im hereeeee");
            }
        }
    }

    void ToggleDoor()
    {
        isOpen = !isOpen;

        // Play sound based on the state of the door
        if (isOpen)
        {
            audioPlayer.PlayOneShot(openSound);
            doorAnimator.SetBool("Open", true);
            doorAnimator.SetBool("Closed", false);
        }
        else
        {
            audioPlayer.PlayOneShot(closeSound);
            doorAnimator.SetBool("Closed", true);
            doorAnimator.SetBool("Open", false);
        }
    }
}