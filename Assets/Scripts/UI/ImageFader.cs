using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ImageFader : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float targetShowAlpha = 1f;
    [SerializeField] private float targetHideAlpha = 0f;
    [SerializeField] private float duration = 1f;
    Coroutine fadingCoroutine;

    private void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }
    public void Show()
    {
        if (image == null) return;
        if (fadingCoroutine != null)
        {
            StopCoroutine(fadingCoroutine);
        }
        fadingCoroutine = StartCoroutine(FadeCoroutine(targetShowAlpha, duration));
        
    }
    IEnumerator FadeCoroutine(float targetAlpha, float duration)
    {
        float startAlpha = image.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            Color color = image.color;
            color.a = newAlpha;
            image.color = color;
            yield return null;
        }

        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
    }
    public void Hide()
    {
        if (fadingCoroutine != null)
        {
            StopCoroutine(fadingCoroutine);
        }
        fadingCoroutine = StartCoroutine(FadeCoroutine(targetHideAlpha,duration));
        
    }
}
