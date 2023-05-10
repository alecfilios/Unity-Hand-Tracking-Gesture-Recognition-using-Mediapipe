using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour
{
    TMP_Text text;

    [Header("Periodic fading")]
    public bool isPeriodicallyFading;
    public float fadeDuration; // Duration of fade in seconds
    public float fadeInterval; // Interval between fades in seconds

    private float fadeTimer = 0.0f; // Timer for tracking fade duration
    private bool isFading = false; // Flag to indicate if text is currently fading in or out

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (isPeriodicallyFading)
        {
            TriggerPressAnywhereText();
        }
    }

    void TriggerPressAnywhereText()
    {

        // Check if it's time to start a new fade
        if (!isFading && fadeTimer >= fadeInterval)
        {
            if (text.color.a == 0)
            {
                // Start the next fade-in
                StartCoroutine(FadeInText());
            }
            else
            {
                // Start the first fade-out
                StartCoroutine(FadeOutText());
            }
        }
        // Update the fade timer
        fadeTimer += Time.deltaTime;
    }

    public void CallFadeIn()
    {
        StartCoroutine(FadeInText());
    }

    IEnumerator FadeInText()
    {
        isFading = true;

        // Set initial alpha to 0
        Color startColor = text.color;
        Color endColor = startColor;
        endColor.a = 1.0f;
        text.color = startColor;

        // Fade in the text over the specified duration
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            text.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set final alpha to 1
        text.color = endColor;

        // Reset the fade timer and start the next fade
        fadeTimer = 0.0f;
        isFading = false;
    }

    public void CallFadeOut()
    {
        StartCoroutine(FadeOutText());
    }

    IEnumerator FadeOutText()
    {
        isFading = true;

        // Set initial alpha to 1
        Color startColor = text.color;
        Color endColor = startColor;
        endColor.a = 0.0f;
        text.color = startColor;

        // Fade out the text over the specified duration
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            text.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set final alpha to 0
        text.color = endColor;

        // Reset the fade timer and start the next fade
        fadeTimer = 0.0f;
        isFading = false;

    }
}
