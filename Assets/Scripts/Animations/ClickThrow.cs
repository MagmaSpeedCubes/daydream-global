using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
public class ClickThrow : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float throwAngle = 45f;
    [SerializeField] private bool followMouse = true;

    private Vector3 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;




        Throw(throwForce, throwAngle);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }


    public void Throw(float force, float angle)
    {
        if (followMouse)
        {
            angle = Mathf.Atan2(originalPosition.y - rectTransform.position.y, originalPosition.x - rectTransform.position.x) * Mathf.Rad2Deg + 180f;
        }        
        Debug.Log("Throwing with force: " + force + " at angle: " + angle);
        // Convert angle to radians
        float rad = angle * Mathf.Deg2Rad;
        // Calculate velocity components
        Vector2 velocity = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * force * 1920;
        StartCoroutine(ThrowCoroutine(velocity));
    }

    private IEnumerator ThrowCoroutine(Vector2 velocity)
    {
        float duration = 4f; // How long the throw lasts
        float elapsed = 0f;
        while (elapsed < duration)
        {
            rectTransform.anchoredPosition += velocity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Destroy the object after the throw
        Destroy(gameObject);
    }
}
