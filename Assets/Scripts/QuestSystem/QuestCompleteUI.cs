using UnityEngine;
using TMPro;

public class QuestCompleteUI : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    void Start()
    {
        SetAlpha(0);
    }

    public void ShowMessage(string message)
    {
        questText.text = message;
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
        float startAlpha = questText.color.a;
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
        var color = questText.color;
        color.a = alpha;
        questText.color = color;
    }
}