using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMinigame : MonoBehaviour
{
    bool started = false;
    bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMinigame(){
        if (!started){
            started = true;
            SceneManager.LoadScene("2D Minigame", LoadSceneMode.Additive);
        }
    }
}
