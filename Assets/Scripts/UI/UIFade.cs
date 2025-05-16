using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    public Image imageToFade;
    public bool fadeIn = true;

    private void Start()
    {
        // Start the fade-in effect
        StartCoroutine(FadeImage(fadeIn));
    }

    public IEnumerator FadeImage(bool fadeIn)
    {
        // Determine target alpha based on fade in/out
        float targetAlpha = fadeIn ? 1f : 0f;
        // Current alpha of the image
        float currentAlpha = imageToFade.color.a;
        // Calculate the speed based on duration
        float fadeSpeed = Mathf.Abs(currentAlpha - targetAlpha) / fadeDuration;

        // Loop until the alpha reaches the target
        while (!Mathf.Approximately(imageToFade.color.a, targetAlpha))
        {
            // Calculate the new alpha value
            float newAlpha = Mathf.MoveTowards(imageToFade.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            // Apply the new alpha value to the image's color
            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, newAlpha);
            // Wait for the next frame
            yield return null;
        }

        // Ensure the final alpha value is set
        imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, targetAlpha);
    }
}

