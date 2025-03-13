using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    GameObject LoadingScreen;

    [SerializeField]
    Slider loadBar;

    public void LoadScene()
    {
        LoadScene(sceneName);
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSceneScreen()
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string name)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / .9f);

            loadBar.value = progressValue;
            
            yield return null;
        }

    }
}
