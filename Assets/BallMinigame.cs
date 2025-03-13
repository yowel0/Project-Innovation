using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BallMinigame : MonoBehaviour
{
    [SerializeField]
    MeshRenderer mr;
    [SerializeField]
    Material[] materials;
    bool started = false;
    bool finished = false;
    [SerializeField]
    UnityEvent OnStart;
    [SerializeField]
    UnityEvent OnFinish;
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
        OnStart?.Invoke();
        if (!started){
            SetMaterial(1);
            started = true;
            SceneManager.LoadScene("2D Minigame", LoadSceneMode.Additive);
        }
    }

    public void FinishMinigame(){
        OnFinish?.Invoke();
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
