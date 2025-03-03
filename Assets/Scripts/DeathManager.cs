using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    
    public static event Action OnDeath;

    public static event Action OnCheckpoint;



    public void KillPlayer(string deathMessage)
    {
        // Put the scripts for the death screen here and invoke OnDeath to reset everything
        Debug.Log(deathMessage);
        OnDeath?.Invoke();
    }

    public static DeathManager GetMainManager()
    {
        return mainManager;
    }
    static DeathManager mainManager = null;

    private void Awake()
    {
        if (mainManager == null)
        {
            mainManager = this;

            // subscribe to events here if needed
        }
        else
        {
            Debug.Log("Second game manager destroys itself");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (mainManager == this)
        {
            mainManager = null;
        }
    }


}
