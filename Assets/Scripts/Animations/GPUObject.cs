using UnityEngine;
using UnityEngine.UI;
public class GPUObject : DragDrop
{
    public GPU gpu;

    [SerializeField] private Sprite topView;
    [SerializeField] private Sprite frontView;
    [SerializeField] private Image imageComponent;
    [SerializeField] private float installHeightRatio;
    [SerializeField] private float uninstallHeightRatio;

    override public void OnInstall()
    {
        base.OnInstall();
        imageComponent.sprite = frontView;
        RectTransform rt = imageComponent.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = size.x * installHeightRatio; // Set your desired height
        rt.sizeDelta = size;

    }

    override public void OnRemove()
    {
        base.OnRemove();
        imageComponent.sprite = topView;
        RectTransform rt = imageComponent.GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.y = size.x * uninstallHeightRatio; // Set your desired height
        rt.sizeDelta = size;
    }

}
