using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    protected RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    protected Canvas canvas;
    protected RectTransform zoomRoot; // For zoom scaling
    public string itemType;
    public Socket currentSocket = null;
    public bool dragLocked = false;

    public float lastDropTime;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
                Debug.LogError("No Canvas found for DragDrop!");
        }
    }

private void Start()
{
    Transform t = transform;
    zoomRoot = null;
    while (t != null)
    {
        if (t.name == "BuildZone")
        {
            zoomRoot = t as RectTransform;
            break;
        }
        t = t.parent;
    }
    if (zoomRoot == null)
        Debug.LogError("BuildZone (zoom root) not found in ancestor hierarchy!");
    else
        Debug.Log("BuildZone found for zoom: " + zoomRoot.name);
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
    float zoomScale = zoomRoot != null ? zoomRoot.localScale.x : 1f;
    rectTransform.anchoredPosition += eventData.delta / zoomScale;
    Debug.Log("Zoom scale (BuildZone): " + zoomScale);
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
