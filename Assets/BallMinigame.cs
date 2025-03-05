using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMinigame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            StartMinigame();
        }
    }

    void StartMinigame(){
        SceneManager.LoadScene("2D Minigame", LoadSceneMode.Additive);
    }
}
