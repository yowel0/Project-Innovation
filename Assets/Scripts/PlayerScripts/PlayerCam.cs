using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX; // Sensitivity of horizontal mouse movement
    public float sensY; // Sensitivity of vertical mouse movement

    public Transform orientation; // Reference to the player's orientation transform

    float xRotation; // Current rotation around the X axis (vertical)
    float yRotation; // Current rotation around the Y axis (horizontal)

    private void Start()
    {
        // Lock cursor and hide it when game starts
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get mouse input for rotation
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Adjust horizontal rotation based on mouse movement
        yRotation += mouseX;

        // Adjust vertical rotation based on mouse movement and clamp it within -90 to 90 degrees
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the camera around X and Y axes
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Rotate the player's orientation around Y axis (horizontal rotation)
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
