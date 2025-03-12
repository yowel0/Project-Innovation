using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableKeyCard(){
        WS_Client wsc;
        wsc = FindAnyObjectByType<WS_Client>();
        wsc.SendWebSocketCommand("SetCardAvailable");
    }
}
