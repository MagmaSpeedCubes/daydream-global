using UnityEngine;
using UnityEngine.UI;
public class RAMObject : DragDrop
{
    public RAM ram;

    [SerializeField] private Sprite topView;
    [SerializeField] private Sprite frontView;
    [SerializeField] private Image imageComponent;
    [SerializeField] protected Vector2 installSize;
    [SerializeField] protected Vector2 uninstallSize;

    override public void OnInstall()
    {
        base.OnInstall();
        imageComponent.sprite = topView;
        RectTransform rt = imageComponent.GetComponent<RectTransform>();
        rt.sizeDelta = installSize;

    }

    override public void OnRemove()
    {
        base.OnRemove();
        imageComponent.sprite = frontView;
        RectTransform rt = imageComponent.GetComponent<RectTransform>();
        rt.sizeDelta = uninstallSize;
    }

}
