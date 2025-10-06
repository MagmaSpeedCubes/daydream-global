using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
using TMPro;
using System.Collections;

public class BarChart : MonoBehaviour
{
    [SerializeField] private float barMin = 0f;
    [SerializeField] private float barMax = 100f;
    [SerializeField] private Image bar;
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private TextMeshProUGUI valueText;
    public float value;
    public string label;

    private float initialWidth;

    void Awake()
    {
        bar.rectTransform.anchorMin = new Vector2(0f, bar.rectTransform.anchorMin.y);
        bar.rectTransform.anchorMax = new Vector2(0f, bar.rectTransform.anchorMax.y);
        bar.rectTransform.pivot = new Vector2(0f, bar.rectTransform.pivot.y);
        bar.rectTransform.anchoredPosition = Vector2.zero;

        // Record the initial width of the bar
        initialWidth = bar.rectTransform.sizeDelta.x;

        Reset();
        UpdateBar();
    }

    void UpdateBar()
    {
        float normalized = Mathf.InverseLerp(barMin, barMax, value);
        bar.rectTransform.sizeDelta = new Vector2(initialWidth * normalized, bar.rectTransform.sizeDelta.y);
        valueText.text = value.ToString("0");
    }
    public float GetValue()
    {
        return value;
    }
    public void SetValue(float newValue)
    {
        value = Mathf.Clamp(newValue, barMin, barMax);
        UpdateBar();
    }

    public void AnimateToValue(float targetValue, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateBarValue(targetValue, duration));
    }

    private IEnumerator AnimateBarValue(float targetValue, float duration)
    {
        float startValue = value;
        float distance = targetValue - startValue;
        float rate = distance / duration;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentValue = startValue + rate * elapsed;
            SetValue(currentValue);
            yield return null;
        }
        SetValue(targetValue); // Ensure final value is set
    }

    public void SetLabel(string newLabel)
    {
        label = newLabel;
        labelText.text = label;
    }

    public void setBounds(float min, float max)
    {
        barMin = min;
        barMax = max;
        UpdateBar();
    }

    public void Reset()
    {
        SetValue(barMin);
        SetLabel("");
    }
}