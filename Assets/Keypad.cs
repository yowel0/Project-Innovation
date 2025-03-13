using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    [SerializeField]
    int code;
    [SerializeField]
    UnityEvent OnTryCode;
    [SerializeField]
    UnityEvent OnCorrectCode;
    [SerializeField]
    UnityEvent OnIncorrectCode;
    void OnTriggerEnter(Collider other)
    {
        WS_Client.CodeEntered += TryCode;
        print("enterRange");
    }

    void OnTriggerExit(Collider other)
    {
        WS_Client.CodeEntered -= TryCode;
        print("exitrage");
    }

    void TryCode(int _code){
        OnTryCode?.Invoke();
        if (_code == code){
            print("Correct Code");
            OnCorrectCode?.Invoke();
        }
        else{
            print("wrong code");
            OnIncorrectCode?.Invoke();
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
