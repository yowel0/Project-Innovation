using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoiceManager : MonoBehaviour
{
    [SerializeField]
    WS_Client client;
    
    public void PlayVoice(int voiceId)
    {
        client.StartCall(voiceId);
    }
    void StopVoice()
    {
        //client.StopCall();
    }

    public static VoiceManager GetMainManager()
    {
        return mainManager;
    }
    static VoiceManager mainManager = null;

    private void Awake()
    {
        if (mainManager == null)
        {
            mainManager = this;

            DeathManager.OnDeath += StopVoice;
            // subscribe to events here if needed
        }
        else
        {
            Debug.Log("Second voice manager destroys itself");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (mainManager == this)
        {
            mainManager = null;
            DeathManager.OnDeath -= StopVoice;
        }
    }
}
