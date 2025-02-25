using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;  // Singleton instance
    public FadeEffect fadeEffect;        // Reference to the FadeEffect script

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Create a singleton instance
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerFadeOutIn()
    {
        StartCoroutine(HandleFadeOutIn());  // Start the fade sequence
    }

    private IEnumerator HandleFadeOutIn()
    {
        yield return StartCoroutine(fadeEffect.FadeOut());
        yield return StartCoroutine(fadeEffect.FadeIn());
    }

    public void TriggerFadeOut()
    {
        StartCoroutine(HandleFadeOut());
    } 

    private IEnumerator HandleFadeOut()
    {
        yield return StartCoroutine (fadeEffect.FadeOut());
    }
}
