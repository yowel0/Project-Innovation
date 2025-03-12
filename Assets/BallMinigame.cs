using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMinigame : MonoBehaviour
{
    [SerializeField]
    MeshRenderer mr;
    [SerializeField]
    Material[] materials;
    bool started = false;
    bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        SetMaterial(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMinigame(){
        if (!started){
            SetMaterial(1);
            started = true;
            SceneManager.LoadScene("2D Minigame", LoadSceneMode.Additive);
        }
    }

    public void FinishMinigame(){
        if (started && !finished){
            SetMaterial(2);
            finished = true;
            SceneManager.UnloadSceneAsync("2D Minigame");
        }
    }

    void SetMaterial(int materialIndex){
        mr.material = materials[materialIndex];
    }
}
