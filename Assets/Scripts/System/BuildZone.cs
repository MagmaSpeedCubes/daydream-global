using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class BuildZone : MonoBehaviour
{
    [SerializeField] private Canvas buildZoneUI;

    public void OnTriggerEnter2D(Collider2D other)
    {

        buildZoneUI.enabled = true;
        Debug.Log("Player entered build zone");

    }

    public void OnTriggerExit2D(Collider2D other)
    {

        ExitBuildZone();

    }

    public void ExitBuildZone()
    {
        buildZoneUI.enabled = false;
        Debug.Log("Player exited build zone via button");
    }
}
