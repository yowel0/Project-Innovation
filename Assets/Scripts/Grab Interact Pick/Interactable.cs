using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public event Action OnInteractionFinish;

    public void Interact()
    {
        // if able to interact:
        StartInteraction();
    }
    protected virtual void StartInteraction()
    {

    }

    public void FinishInteraction()
    {
        OnInteractionFinish?.Invoke();
    }
}
