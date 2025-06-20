using System.Collections;
using ArabicSupport;
using TMPro;
using UnityEngine;

public class HistoricalPrizeInfo : MonoBehaviour
{
    [SerializeField] private string prizeMessage;
    [SerializeField] private TextMeshProUGUI prizeInfoText;
    private Health health;
    Coroutine fadingCoroutine;
    private void Start()
    {
        health = GetComponent<Health>();
        health.onDie += ShowUIPrizeInfo;
    }
    private void OnDestroy()
    {
        health.onDie -= ShowUIPrizeInfo;
    }
    public void ShowUIPrizeInfo()
    {
        prizeInfoText.text = ArabicFixer.Fix(prizeMessage, true, false);
        if (fadingCoroutine != null) StopCoroutine(fadingCoroutine);
        fadingCoroutine = StartCoroutine(FadeTextToTargetAlpha(0f, 1f, prizeInfoText));
    }
    
    private IEnumerator FadeTextToTargetAlpha(float targetAlpha,float duration, TextMeshProUGUI text)
    {
        Color originalColor = new Color(text.color.r,text.color.g,text.color.b,1f);
        yield return new WaitForSeconds(duration);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, targetAlpha, elapsed / duration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure alpha is zero at the end
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }
}
