using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
public class Screwable : MonoBehaviour, IPointerDownHandler
{
    //This script is for the collide, not the actual object
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Image screwingPart;
    private float initialAngle;
    [SerializeField] private float revolutions = 3f;
    [SerializeField] private float screwDuration = 2f;
    private bool isScrewing = false;
    public bool isScrewed = false;
    [SerializeField] private AudioClip screwSound;
    [SerializeField] private bool reversible = true;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown on Screwable");
        if (!isScrewing)
        {
            initialAngle = rectTransform.eulerAngles.z;
            StartCoroutine(ScrewCoroutine());
        }
    }

    IEnumerator ScrewCoroutine()
    {
        isScrewing = true;
        float targetAngle = initialAngle + 360f * revolutions;
        //convert to radians
        float elapsed = 0.4f;

        int screwDirection = isScrewed ? -1 : 1; // Reverse direction if unscrewing
        //place screw down if being screwed in
        if (!isScrewed)
        {
            StartCoroutine(LiftScrewCoroutine(1f, 0.4f));
        }

        while (elapsed < screwDuration)
        {
            float angle = Mathf.Lerp(initialAngle, targetAngle, elapsed / screwDuration);
            rectTransform.eulerAngles = new Vector3(0f, 0f, angle);
            screwingPart.rectTransform.eulerAngles = new Vector3(0f, 0f, -angle  * screwDirection); // Counter-rotate the part being screwed
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.eulerAngles = new Vector3(0f, 0f, targetAngle);
        screwingPart.rectTransform.eulerAngles = new Vector3(0f, 0f, -targetAngle * screwDirection); // Final counter-rotation

        //lift the screw out if being unscrewed
        if (isScrewed)
        {
            StartCoroutine(LiftScrewCoroutine(1f, 2.5f));
        }

        isScrewing = false;
        isScrewed = !isScrewed;
        if (!reversible)
        {
            canvasGroup.blocksRaycasts = isScrewed;
            canvasGroup.interactable = false;
            this.enabled = false;
        }


        yield return null;
    }

    IEnumerator LiftScrewCoroutine(float duration, float scale)
    {
        //this is a ui layer, so simulate lifting by making the screw larger
        Vector3 originalScale = screwingPart.rectTransform.localScale;
        Vector3 targetScale = originalScale * scale;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            screwingPart.rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
            Color c = screwingPart.color;
            c.a = Mathf.Lerp(c.a, scale > 1f ? 0f : 1f, elapsed / duration);
            screwingPart.color = c;
        }
        screwingPart.rectTransform.localScale = targetScale;
        yield return null;

        if (scale > 1f)
        {
            Color c = screwingPart.color;
            c.a = 0f;
            screwingPart.color = c;
        }
        else
        {
            Color c = screwingPart.color;
            c.a = 1f;
            screwingPart.color = c;
        }
    }

}
