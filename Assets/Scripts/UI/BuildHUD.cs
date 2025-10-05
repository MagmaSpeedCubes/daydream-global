using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class BuildHUD : MonoBehaviour
{
    [SerializeField] private Vector2 hideAnchoredPosition;
    [SerializeField] private Vector2 showAnchoredPosition;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Sprite[] icons;
    [SerializeField] private Canvas BuildHUDCanvas;
    [SerializeField] private Image infoPanel;
    [SerializeField] private Image infoIcon;
    [SerializeField] private TextMeshProUGUI infoTitle;
    [SerializeField] private TextMeshProUGUI infoText;
    

    public void ShowPopup(string type, string title, string text, int duration)
    {
        StartCoroutine(ShowPopupCoroutine(type, title, text, duration));
    }
    private IEnumerator ShowPopupCoroutine(string type, string title, string text, int duration)
    {
        Debug.Log("ShowPopupCoroutine started");
        BuildHUDCanvas.enabled = true;
        infoPanel.enabled = true;
        infoTitle.text = title;
        infoText.text = text;

        switch (type)
        {
            case "Info":
                infoIcon.sprite = icons[0];
                infoPanel.color = new Color(0.6f, 0.6f, 0.6f, 0.8f);
                break;
            case "Warning":
                infoIcon.sprite = icons[1];
                infoPanel.color = new Color(1f, 0.92f, 0.016f, 0.8f);
                break;
            case "Error":
                infoIcon.sprite = icons[2];
                infoPanel.color = new Color(1f, 0.16f, 0.016f, 0.8f);
                break;
            default:
                infoIcon.sprite = null;
                infoPanel.color = new Color(0.6f, 0.6f, 0.6f, 0.8f);
                break;
        }
        Debug.Log("Beginning popup animation");
        yield return StartCoroutine(MovePopup(showAnchoredPosition, 0.5f));
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(MovePopup(hideAnchoredPosition, 0.5f));
        yield return null;
        Debug.Log("Popup animation complete, hiding HUD");
        BuildHUDCanvas.enabled = false;

    }


    public IEnumerator MovePopup(Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = infoPanel.rectTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            infoPanel.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        infoPanel.rectTransform.anchoredPosition = targetPosition;
    }
}
