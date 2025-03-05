using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField]
    private Transform checkpointLocation;

    [SerializeField]
    private bool isActivated;

    public void SetCheckpoint()
    {
        if (isActivated) return;
        PlayerMovement.GetPlayer().SetRespawn(checkpointLocation.position);
        DeathManager.GetMainManager().CheckpointReached();
        isActivated = true;
        transform.Translate(0, 1, 0);   // showing you hit the checkpoint, remove later
    }

}
