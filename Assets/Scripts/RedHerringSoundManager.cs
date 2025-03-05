using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RedHerringSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    [SerializeField] bool disableSounds;

    [Header("Waiting time in seconds")]
    [SerializeField] float minInterval;
    [SerializeField] float maxInterval;

    [Header("Number of duplicates allowed")]
    [SerializeField] int maxRepeat;

    [Header("Set by code")]
    [SerializeField] AudioSource aSource;

    [SerializeField] float chosenInterval;
    [SerializeField] float intervalProgress;

    [SerializeField] int lastPlayed;
    [SerializeField] int repeatCount;

    
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        RandomizeInterval();
    }


    private void FixedUpdate()
    {
        if (disableSounds) return;
        intervalProgress += Time.fixedDeltaTime;

        if (intervalProgress >= chosenInterval)
        {
            PlaySound();
            RandomizeInterval();
        }
    }

    void PlaySound()
    {
        int nr = GetRandomSoundNr();


        aSource.PlayOneShot(sounds[nr]);
    }

    int GetRandomSoundNr()
    {
        int soundAmount = sounds.Length;

        int nr = Random.Range(0, soundAmount);

        if (nr == lastPlayed && soundAmount > 1)
        {
            repeatCount++;
            if (repeatCount > maxRepeat)
            {
                // setting nr to a different random sound
                int otherNr = Random.Range(0, soundAmount - 1);
                if (otherNr == nr) otherNr++;

                nr = otherNr % soundAmount;
                repeatCount = 0;
            }
        }
        else
        {
            repeatCount = 0;
        }

        lastPlayed = nr;

        return nr;
    }

    void RandomizeInterval()
    {
        if (minInterval > maxInterval)
        {
            Debug.LogWarning("Min interval is higher than max, the chosen interval will not be accurate");
        }
        chosenInterval = Random.value * (maxInterval - minInterval) + minInterval;
        intervalProgress = 0f;
    }
}
