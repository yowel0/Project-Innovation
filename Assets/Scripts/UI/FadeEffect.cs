using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage; // Drag the UI Image here in the Inspector
    public float fadeDuration = 1f; // Duration of the fade

    void Start()
    {
        //StartCoroutine(FadeIn()); // Automatically fade in at the start
        fadeImage.gameObject.SetActive(false);
        DeathManager.OnRespawn += HideScreen;
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color fadeColor = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration)); // Decrease alpha
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // Disable the image after fading
    }

    public IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true); // Enable the image before fading
        float elapsedTime = 0f;
        Color fadeColor = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(elapsedTime / fadeDuration); // Increase alpha
            fadeImage.color = fadeColor;
            yield return null;
        }
    }

    void HideScreen()
    {
        fadeImage.gameObject.SetActive(false);
    }
}
