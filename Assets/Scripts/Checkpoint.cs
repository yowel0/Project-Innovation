using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{

    [SerializeField]
    private Transform checkpointLocation;

    [SerializeField]
    private bool isActivated;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip audioClip;
    [SerializeField]
    UnityEvent OnFinishSave;
    float timer;
    [SerializeField]
    float timerMax = 2;

    public void SetCheckpoint()
    {
        if (isActivated) return;
        PlayerMovement.GetPlayer().SetRespawn(checkpointLocation.position);
        DeathManager.GetMainManager().CheckpointReached();
        isActivated = true;
        //transform.Translate(0, 1, 0);   // showing you hit the checkpoint, remove later
        timer = timerMax;
        audioSource.Play();
    }

    void Update()
    {
        if (timer > 0){
            timer -= Time.deltaTime;
            if (timer <= 0){
                OnFinishSave?.Invoke();
            }
        }
    }

}
