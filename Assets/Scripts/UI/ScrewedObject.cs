using UnityEngine;
using UnityEngine.EventSystems;
public class ScrewedObject : DragDrop
{
    [SerializeField] private Screwable[] screws;

    //this is a regular object with the difference that it can only be dragged if all screwes are unscrewed
    public bool screwLocked = false;
    void Update()
    {
        bool allUnscrewed = true;
        foreach (Screwable screw in screws)
        {
            if (screw.isScrewed)
            {
                allUnscrewed = false;
                break;
            }
        }
        screwLocked = !allUnscrewed;

    }

    override public void OnDrag(PointerEventData eventData)
    {
        if (dragLocked || screwLocked) { Debug.Log("Drag is locked, cannot move"); return; }
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
