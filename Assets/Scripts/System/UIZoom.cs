using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIZoom : MonoBehaviour
{
    [SerializeField] private CanvasScaler canvasScaler;
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 2f;
    [SerializeField] private RectTransform panRoot; // Assign in inspector



    private float currentScale = 1f;
    private bool isPanning = false;
    private Vector2 lastMousePosition;

    void Start()
    {
        if (canvasScaler == null)
            canvasScaler = GetComponent<CanvasScaler>();
        currentScale = canvasScaler.scaleFactor;
    }

    void Update()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll != 0)
        {
            Zoom(scroll > 0 ? zoomSpeed : -zoomSpeed);
        }
        if (Keyboard.current.equalsKey.wasPressedThisFrame || Keyboard.current.numpadPlusKey.wasPressedThisFrame)
        {
            Zoom(zoomSpeed);
        }
        if (Keyboard.current.minusKey.wasPressedThisFrame || Keyboard.current.numpadMinusKey.wasPressedThisFrame)
        {
            Zoom(-zoomSpeed);
        }

        // Pan with middle mouse button or shift + left mouse button
        bool middleMouse = Mouse.current.middleButton.isPressed;
        bool shiftLeftMouse = Keyboard.current.leftShiftKey.isPressed && Mouse.current.leftButton.isPressed;

        if ((middleMouse || shiftLeftMouse) && !isPanning)
        {
            Debug.Log("Start Panning");
            isPanning = true;
            lastMousePosition = Mouse.current.position.ReadValue();
        }
        else if (!(middleMouse || shiftLeftMouse) && isPanning)
        {
            isPanning = false;
        }

        if (isPanning)
        {
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            Vector2 delta = currentMousePosition - lastMousePosition;

            panRoot.anchoredPosition += delta;
            lastMousePosition = currentMousePosition;
        }
    }

    void Zoom(float delta)
    {
        currentScale = Mathf.Clamp(currentScale + delta, minScale, maxScale);
        canvasScaler.scaleFactor = currentScale;
    }
}