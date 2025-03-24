using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rock : MonoBehaviour
{
    public Transform canvas;
    public GameObject fadePrefab;
    GameObject fadeObject;

    bool timerStarted = false;
    float timer;
    bool interacted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(timer);
        if (timerStarted){
            timer += Time.deltaTime;
        }
        if (fadeObject == null && timer >= 27){
            fadeObject = Instantiate(fadePrefab,canvas);
        }
        if (timer >= 31){
            SceneManager.LoadScene("EndScreen");
        }
    }

    public void InteractWithRock(){
        if (!interacted){
            interacted = true;
            timerStarted = true;
            StartLastCall();
        }

    }

    void StartLastCall(){
        WS_Client wsc;
        wsc = FindAnyObjectByType<WS_Client>();
        wsc.StartCall(1);
    }
}
