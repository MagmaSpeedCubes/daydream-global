using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System;
public class ScrewedSocket : Socket
{
    public bool screwLocked = false;
    [SerializeField] protected Screwable[] screws;
    //a regular socket but with screws that can be screwed in to lock the item in place
    
    void Update()
    {
        screwLocked = false;
        foreach (Screwable screw in screws)
        {
            if (screw.isScrewed)
            {
                screwLocked = true;
                break;
            }
        }

        if (installedComponent != null)
        {
            if (currentIndex == removeIndex && !screwLocked)
            {
                installedComponent.UnlockDrag();
            }
            else
            {

                installedComponent.LockDrag();

            }
        }
        //check if 
        // all screws are screwed in

    }

    override public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop in Socket");
        var dragDrop = eventData.pointerDrag?.GetComponent<DragDrop>();
        if (dragDrop != null && dragDrop.itemType == componentType && installedComponent == null && currentIndex == installIndex && !screwLocked)
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

            installedComponent.currentSocket = this; // Add this property to DragDrop
            installedComponent.OnInstall();
            
        }
        else
        {
            audioSource.PlayOneShot(errorSound);
            StartCoroutine(HighlightCoroutine(Color.red, 0.2f, 0.5f));
            
        }
    }
    override public void OnRemoveFromSocket()
    {
        if (installedComponent != null && !screwLocked)
        {
            currentIndex -= 1;
            audioSource.PlayOneShot(removeSound);
            installedComponent.OnRemove();
            installedComponent = null;
            Debug.Log("Item removed from socket");
        }
        else
        {
            StartCoroutine(HighlightCoroutine(Color.red, 0.2f, 0.5f));
            buildHUD.ShowPopup("Error", "Cannot Place Item", "Remove all screws before installing item", 3);
        }
    }
}
