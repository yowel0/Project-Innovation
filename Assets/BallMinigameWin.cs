using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallMinigameWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Finish(){
        print("MINIGAME WIN");
        BallMinigame ballMinigame = FindAnyObjectByType<BallMinigame>();
        ballMinigame.FinishMinigame();
    }

    void OnTriggerStay2D(){
        print("triggerstay");
        Finish();
    }
}
