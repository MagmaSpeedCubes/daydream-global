using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(AudioSource))]
public class Socket : MonoBehaviour, IDropHandler
{
    /*
    for ICPU:
    -1 = socket closed empty
    0 = opened, cpu can be installed here
    1 = cpu installed
    2 = socket closed with cpu inside
    3 = thermal paste applied
    4 = cooler mounted
    
    for PCIE:
    0 = opened default, can install accessories
    2 = accessory installed and locked
    1 = accessory installed but can be removed

    for RAM:
    0 = opened default, can install ram
    1 = ram installed 

    for STORAGE:
    0 = opened default, can install storage
    1 = storage installed
    2 = storage installed and screwed down


    */

    [SerializeField] protected string componentType;
    [SerializeField] protected int installIndex;
    [SerializeField] protected int removeIndex;
    [SerializeField] protected int startIndex;
    [SerializeField] protected BuildHUD buildHUD; // reference to the BuildHUD script to display error messages
    [SerializeField] protected string[] errorMessages; // error messages to display when trying to drop an item into a locked socket
    public int currentIndex; //the current status of this socket with 0 being filled 

    [SerializeField] protected AudioClip installSound;
    [SerializeField] protected AudioClip removeSound;
    [SerializeField] protected AudioClip errorSound;
    [SerializeField] protected float verticalOffset = 0f; // Vertical offset for dropped item
    protected AudioSource audioSource;
    protected DragDrop installedComponent;
    

    public float lastDropTime;

    public string[] attributes;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentIndex = startIndex;
    }

    virtual public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop in Socket");
        var dragDrop = eventData.pointerDrag?.GetComponent<DragDrop>();
        if (dragDrop != null && dragDrop.itemType == componentType && installedComponent == null && currentIndex == installIndex)
        {
            // Positioning logic
            Vector2 offset = new Vector2(0, verticalOffset);
            RectTransform droppedRect = dragDrop.GetComponent<RectTransform>();
            RectTransform socketRect = GetComponent<RectTransform>();
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                droppedRect.parent as RectTransform,
                socketRect.position,
                eventData.pressEventCamera,
                out localPoint
            );
            droppedRect.anchoredPosition = localPoint + offset;

            // Install logic
            currentIndex++;
            audioSource.PlayOneShot(installSound);
            installedComponent = dragDrop;
            installedComponent.OnInstall();
            installedComponent.currentSocket = this; // Add this property to DragDrop
            
        }
        else
        {
            StartCoroutine(HighlightCoroutine(Color.red, 0.2f, 0.5f));
            buildHUD.ShowPopup("Error", "Cannot Place Item", errorMessages[Mathf.Clamp(currentIndex - startIndex, 0, errorMessages.Length - 1)], 3);
        }
    }

    // Called by DragDrop when the object is picked up (removed from socket)
    virtual public void OnRemoveFromSocket()
    {
        if (installedComponent != null)
        {
            currentIndex --;
            audioSource.PlayOneShot(removeSound);
            installedComponent.OnRemove();
            installedComponent = null;
            Debug.Log("Item removed from socket");
        }
    }

    void Update()
    {
        if (installedComponent != null)
        {
            if (currentIndex == removeIndex)
            {
                installedComponent.UnlockDrag();
            }
            else
            {

                installedComponent.LockDrag();

            }
        }

        // if (installedComponent != null)
        // {
        //     float idt = installedComponent.lastDropTime;
        //     if(Math.Abs(idt - lastDropTime) > 0.2f)
        //     {
        //         //detects if there is a difference in last drop time, meaning the item has been dropped again
        //         RemoveItem();
        //     }
        // }
    }

    protected IEnumerator HighlightCoroutine(Color highlightColor, float fadeDuration, float highlightDuration)
    {
        Image sr = GetComponent<Image>();
        Color originalColor = sr.color;

        // Fade to highlight color
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            sr.color = Color.Lerp(originalColor, highlightColor, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = highlightColor;

        // Hold highlight color
        yield return new WaitForSeconds(highlightDuration);

        // Fade back to original color
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            sr.color = Color.Lerp(highlightColor, originalColor, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = originalColor;
    }

    public void Lock()
    {

    }
    
    
    
    


}
