using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadbobController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool _enable = true; // Toggle to enable/disable headbobbing

    [SerializeField] private float _amplitude = 0.015f; // Amplitude of headbobbing motion
    [SerializeField] private float _frequency = 10.0f; // Frequency of headbobbing motion
    [SerializeField] private float _resetCamSpeed = 1; // Frequency of headbobbing motion

    [Header("Holders")]
    [SerializeField] private Transform _camera = null; // Reference to the main camera
    [SerializeField] private Transform _cameraHolder = null; // Reference to the camera holder
    [SerializeField] private Transform _orientation = null; // Reference to the orientation object (e.g., player)

    private float _toggleSpeed = 2.0f; // Speed threshold to trigger headbobbing
    private Vector3 _startPos; // Starting position of the camera
    private PlayerMovement _controller; // Reference to the PlayerMovement script

    private void Start()
    {
        _controller = GetComponent<PlayerMovement>(); // Get the PlayerMovement component attached to the same GameObject
        _startPos = _camera.localPosition; // Store the initial local position of the camera
    }

    void Update()
    {
        if (!_enable) return; // If headbobbing is disabled, exit Update

        CheckMotion(); // Check if the player's motion should trigger headbobbing
        ResetPosition(); // Reset the camera position smoothly back to the start position
        _camera.LookAt(FocusTarget()); // Make the camera look at a specific target point
    }

    // Calculate the headbobbing motion based on footstep-like movement
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude; // Vertical motion (bobbing up and down)
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2; // Horizontal motion (swaying side to side)
        return pos;
    }

    // Check if player's motion is sufficient to trigger headbobbing
    private void CheckMotion()
    {
        float speed = _controller.GetMovementSpeed(); // Get player's movement speed directly
        if (speed < _toggleSpeed) return; // If speed is below threshold, do not trigger headbobbing

        PlayMotion(FootStepMotion()); // Trigger headbobbing based on footstep motion
    }


    // Apply the calculated motion to the camera, relative to player's orientation
    private void PlayMotion(Vector3 motion)
    {
        Vector3 localMotion = _orientation.TransformDirection(motion); // Transform motion to be relative to player's orientation
        _camera.localPosition += localMotion; // Apply the motion to the camera's local position
    }

    // Calculate the target position for the camera to look at
    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z); // Calculate position slightly above player's position
        pos += _cameraHolder.forward * 7.0f; // Offset forward by a fixed distance
        return pos; // Return the calculated target position
    }

    // Smoothly reset the camera's position back to the starting position
    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return; // If camera is already at the start position, do nothing

        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, _resetCamSpeed * Time.deltaTime); // Smoothly move camera towards the start position
    }
}
