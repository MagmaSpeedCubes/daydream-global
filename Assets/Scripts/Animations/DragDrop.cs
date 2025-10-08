using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    protected RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] protected Canvas canvas;
    public string itemType;
    public Socket currentSocket = null;
    public bool dragLocked = false;

    public float lastDropTime;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        if (currentSocket != null)
        {
            currentSocket.OnRemoveFromSocket();
            currentSocket = null;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;

    }
    virtual public void OnDrag(PointerEventData eventData)
    {
        if (dragLocked) { Debug.Log("Drag is locked, cannot move"); return; }
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        lastDropTime = Time.time;
    }

    virtual public void OnInstall()
    {

    }

    virtual public void OnRemove()
    {

    }

    public void LockDrag()
    {
        dragLocked = true;
        Debug.Log("Drag locked");
    }
    public void UnlockDrag()
    {
        dragLocked = false;
        Debug.Log("Drag unlocked");
    }
}
