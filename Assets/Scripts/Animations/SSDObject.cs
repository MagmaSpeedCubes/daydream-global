using UnityEngine;
using UnityEngine.UI;
public class SSDObject : DragDrop
{
    public Storage storage;

    [SerializeField] private Image imageComponent;

    override public void OnInstall()
    {
        base.OnInstall();
        if (currentSocket.attributes != null && System.Array.Exists(currentSocket.attributes, element => element == "LeftNVMe"))
        {
            imageComponent.transform.localRotation = Quaternion.Euler(0, 0, -180); ;
        }
        else if (currentSocket.attributes != null && System.Array.Exists(currentSocket.attributes, element => element == "RightNVMe"))
        {
            imageComponent.transform.localRotation = Quaternion.Euler(0, 0, 0); ;
        }
        else
        {
            Debug.LogError("SSD installed in a socket without LeftNVMe or RightNVMe attribute!");
        }

    }

    override public void OnRemove()
    {
        base.OnRemove();

    }

}
