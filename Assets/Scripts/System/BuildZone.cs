using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class BuildZone : MonoBehaviour
{
    [SerializeField] private Canvas buildZoneUI;
    [SerializeField] private BuildHUD buildHUD;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (RoundStats.partsSelected == false)
        {
            buildHUD.ShowPopup("Info", "Build Zone", "Select parts to build your PC!", 3);
            return;
        }
        else
        {
            buildZoneUI.enabled = true;
            Debug.Log("Player entered build zone");
        }



    }

    public void OnTriggerExit2D(Collider2D other)
    {

        ExitBuildZone();

    }

    public void ExitBuildZone()
    {
        buildZoneUI.enabled = false;
        Debug.Log("Player exited build zone");
    }
}
