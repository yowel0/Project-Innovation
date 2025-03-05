using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorExample : MonoBehaviour
{
    public UnityEvent OnDoorOpen;

    [SerializeField]
    bool isOpen;
    
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        DeathManager.OnRespawn += Respawn;
        DeathManager.OnCheckpoint += SaveCurrentState;
    }

    
    public void OpenDoorCheck()
    {
        if (isOpen) return;
        isOpen = true;

        OnDoorOpen?.Invoke();

    }

    public void OpenDoor()
    {
        transform.Translate(1, 1, 1);
        //aSource.Play();

    }

    private void Respawn()
    {
        transform.position = startPos;
        isOpen = false;
    }

    private void SaveCurrentState()
    {
        if (isOpen)
        {
            DeathManager.OnRespawn -= Respawn;
            DeathManager.OnCheckpoint -= SaveCurrentState;
        }
    }
}
