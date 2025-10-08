using UnityEngine;

public class HideOnOpen : MonoBehaviour
{
    [SerializeField] private Canvas[] canvasesToHide;
    void Awake()
    {
        foreach (Canvas canvas in canvasesToHide)
        {
            canvas.enabled = false;
        }
    }
}
