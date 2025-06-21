using UnityEngine;
using TMPro;

public class QuestCompleteUI : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public TextMeshProUGUI progressText;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    void Start()
    {
        SetAlpha(questText, 0);
        SetAlpha(progressText, 0);
    }

    private void ShowMessage(TextMeshProUGUI text,string message)
    {
        text.text = message;
        StopAllCoroutines();
        StartCoroutine(FadeInOut(text));
    }
    public void ShowQuestMessage(string message)
    {
        ShowMessage(questText,message);
    }
    public void ShowProgressMessage(string message)
    {
        ShowMessage(progressText,message);
    }

    private System.Collections.IEnumerator FadeInOut(TextMeshProUGUI text)
    {
        yield return FadeTo(text, 1f, fadeDuration);
        yield return new WaitForSeconds(displayDuration);
        yield return FadeTo(text, 0f, fadeDuration);
    }

    private System.Collections.IEnumerator FadeTo(TextMeshProUGUI text,float targetAlpha, float duration)
    {
        float startAlpha = text.color.a;
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            SetAlpha(text,alpha);
            time += Time.deltaTime;
            yield return null;
        }

        SetAlpha(text,targetAlpha);
    }

    private void SetAlpha(TextMeshProUGUI text,float alpha)
    {
        var color = text.color;
        color.a = alpha;
        text.color = color;
    }
}