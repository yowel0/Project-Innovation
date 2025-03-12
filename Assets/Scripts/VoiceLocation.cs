using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLocation : MonoBehaviour
{
    [SerializeField]
    int voiceID;

    public bool canBeInteractedWith;
    [SerializeField] 
    bool canBeInteractedWithSaved;

    [SerializeField]
    bool hasPlayed;

    private void Start()
    {
        DeathManager.OnCheckpoint += SaveState;
        DeathManager.OnRespawn += ResetState;
    }

    public void EnableInteraction()
    {
        canBeInteractedWith = true;
    }
    public void DisableInteraction()
    {
        // Mainly meant for disabling the girl's voice lines if the guy takes over
        canBeInteractedWith = false;
    }

    void SaveState()
    {
        if (hasPlayed)
        {
            DeathManager.OnCheckpoint -= SaveState;
            DeathManager.OnRespawn -= ResetState;
        }
        canBeInteractedWithSaved = canBeInteractedWith;
 
    }
    void ResetState()
    {
        canBeInteractedWith = canBeInteractedWithSaved;
        hasPlayed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canBeInteractedWith && !hasPlayed)
        {
            Debug.Log("Playing sound " + voiceID);
            hasPlayed = true;
            VoiceManager.GetMainManager().PlayVoice(voiceID);
        }
    }
}
