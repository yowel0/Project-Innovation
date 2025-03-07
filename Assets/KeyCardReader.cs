using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyCardReader : MonoBehaviour
{
    [SerializeField]
    int cardID = 0;
    [SerializeField]
    UnityEvent OnCorrectCode;
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
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
