using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExample : MonoBehaviour
{
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

    
    public void OpenDoor()
    {
        if (isOpen) return;
        transform.Translate(1, 1, 1);
        isOpen = true;
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
