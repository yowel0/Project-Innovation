using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using WebSocketSharp;

public class WS_Client : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject plane;
    WebSocket ws;
    private readonly ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();
    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();
        Debug.Log("connection status: " + ws.IsAlive);
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received: " + e.Data);
            if (e.Data.StartsWith("command:")){
                _actions.Enqueue(() => ProcessCommand(e.Data));
            }
            else if (e.Data.StartsWith("value:")){
                _actions.Enqueue(() => ProcessValue(e.Data));
            }
        };  
    }

    void ProcessCommand(string _command){
        string command = _command.Replace("command:","");
        print("command recognized: " + command);
        switch (command)
        {
            case ("ActivatePlane"):
                print("ActivatePlane");
                plane.SetActive(!plane.activeInHierarchy);
            break;
            case ("SpawnGameobject"):
                SpawnGameObject();
                print("gambeobjeccooo");
            break;
        }
    }
    void ProcessValue(string _value){
        string value = _value.Replace("value:","");
        print("value recognized: " + value);
    }

    void SpawnGameObject(){
        Instantiate(cubePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null){
            Debug.Log("ws is null");
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            ws.Send("Hellosent");
            //SpawnGameObject();
        }

        while (_actions.Count > 0){
            if(_actions.TryDequeue(out var action)){
                action?.Invoke();
            }
        }
    }
}
    