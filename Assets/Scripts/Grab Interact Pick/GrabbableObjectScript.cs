using System;
using UnityEngine;

public class GrabbableObjectScript : MonoBehaviour
{
    // Change objectID from int to string
    public string objectID = ""; // String ID now
    public bool hasPhysics = true;

    public string grabDialogue = "";
    public float dialogueTime = 5f;
    bool dialogueHasPlayed;

    public AudioClip[] grabSFX;
    public AudioClip placeSFX = null;
    AudioSource audioPlayer;

    private GameObject PlacedOnPlacable = null;

    public GameObject placedOnPlacable
    {
        get
        {
            return PlacedOnPlacable;
        }
        set
        {
            if (value != null && value.TryGetComponent(out PlacerScript script))
            {
                PlaySound(placeSFX);
                if (script.placerLinkIDs.Length > 0)
                {
                    for (int i = 0; i < script.placerLinkIDs.Length; i++)
                    {
                        if (script.placerLinkIDs[i] == objectID) isPlacedRight = true;
                    }
                }
            }
            else
            {
                int nr = UnityEngine.Random.Range(0, grabSFX.Length);
                AudioClip sound = grabSFX[nr];
                Debug.Log("Playing grab sound " + nr);
                PlaySound(sound);
                isPlacedRight = false;
                if (!dialogueHasPlayed && grabDialogue != "")
                {
                    DialogueSystem.GetMainDialogueSystem().HandleText(grabDialogue, dialogueTime);
                    dialogueHasPlayed = true;
                }
            }
            PlacedOnPlacable = value;
        }
    }

    bool IsPlacedRight = false;
    bool isPlacedRight
    {
        get
        {
            return IsPlacedRight;
        }
        set
        {
            if (value != IsPlacedRight)
            {
                if (value)
                {
                    GameManager.GetMainManager().CorrectObjectIDLink(objectID); // Pass string objectID
                }
                else
                {
                    GameManager.GetMainManager().WrongObjectIDLink(objectID); // Pass string objectID
                }
            }
            IsPlacedRight = value;
        }
    }

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip sound)
    {
        if (audioPlayer != null && sound != null)
        {
            audioPlayer.PlayOneShot(sound);
        }
        else
        {
            Debug.LogWarning("Cannot play sound. Either audioPlayer or sound is missing.");
        }
    }
}
