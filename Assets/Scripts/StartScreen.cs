using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public string sceneName = "Stage 1";
    public void StartGame(int fadeDuration)
    {
        Debug.Log("Trying to start game");
        FadeManager.instance.TriggerFadeOutIn();
        Invoke(nameof(LoadScene), fadeDuration);
    }

    public void StopGame()
    {
        Debug.Log("Stopping game");
        Application.Quit();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);

    }
}
