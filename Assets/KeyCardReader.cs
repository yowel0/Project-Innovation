using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KeyCardReader : MonoBehaviour
{
    [SerializeField]
    int cardID = 0;
    [SerializeField]
    UnityEvent OnCorrectCode;
    [SerializeField]
    UnityEvent OnWrongCode;
    [SerializeField]
    UnityEvent SwipeFail;
    void OnTriggerEnter(Collider other)
    {
        WS_Client.CardScanned += TryCard;
        print("enterRange");
    }

    void OnTriggerExit(Collider other)
    {
        WS_Client.CodeEntered -= TryCard;
        print("exitrage");
    }

    void TryCard(int _cardID){
        if (_cardID == cardID){
            print("Correct Card");
            OnCorrectCode?.Invoke();
        }
        else{
            print("wrong card");
            OnWrongCode?.Invoke();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            WS_Client wsc;
            wsc = FindAnyObjectByType<WS_Client>();
            wsc.ws.Send("command:SetCardAvailable");
        }
    }
}
