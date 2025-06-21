using System.Collections;
using ArabicSupport;
using TMPro;
using UnityEngine;

public class HistoricalPrizeInfo : MonoBehaviour
{
    [SerializeField] private string prizeMessage;
    [SerializeField] private TextMeshProUGUI prizeInfoText;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;
    private Health health;
    Coroutine fadingCoroutine;
    private void Start()
    {
        health = GetComponent<Health>();
        health.onDie += ShowMessage;
    }
    private void OnDestroy()
    {
        health.onDie -= ShowMessage;
    }
    
    public void ShowMessage()
    {
        prizeInfoText.text = ArabicFixer.Fix(prizeMessage, true, false);
        StopAllCoroutines();
        StartCoroutine(FadeInOut());
    }

    private System.Collections.IEnumerator FadeInOut()
    {
        yield return FadeTo(1f, fadeDuration);
        yield return new WaitForSeconds(displayDuration);
        yield return FadeTo(0f, fadeDuration);
    }

    private System.Collections.IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = prizeInfoText.color.a;
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            SetAlpha(alpha);
            time += Time.deltaTime;
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        var color = prizeInfoText.color;
        color.a = alpha;
        prizeInfoText.color = color;
    }
}
