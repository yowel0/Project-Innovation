using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using WebSocketSharp;

public class WS_Client : MonoBehaviour
{
    public GameObject plane;
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    WebSocket ws;
    private readonly ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();

    public Action<Quaternion> GyroscopeChanged;
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
                plane.SetActive(!plane.activeInHierarchy);
            break;
            case ("SpawnCube"):
                Instantiate(cubePrefab);;
            break;
            case ("SpawnSphere"):
                Instantiate(spherePrefab);;
            break;
        }
    }
    void ProcessValue(string _value){
        string value = _value.Replace("value:","");
        print("value recognized: " + value);
        if (value.StartsWith("gyroscope:")){
            string gyroscopeString = value.Replace("gyroscope:","");
            float[] floats = Array.ConvertAll(gyroscopeString.Split(','), float.Parse);
            Quaternion gyroscope = new Quaternion(floats[0],floats[1],floats[2],floats[3]);
            
            GyroscopeChanged?.Invoke(gyroscope);
        }
    }

    void StartCall(int callID){
        ws.Send("phonecall:" + callID);
    }

    // Update is called once per frame
    void Update()
    {
        if (ws == null){
            Debug.Log("ws is null");
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            StartCall(0);
            // ws.Send("Hellosent");
            //SpawnGameObject();
        }

        while (_actions.Count > 0){
            if(_actions.TryDequeue(out var action)){
                action?.Invoke();
            }
        }
    }
}
    