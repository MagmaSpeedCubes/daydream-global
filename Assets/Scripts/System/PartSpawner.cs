using UnityEngine;
using UnityEngine.UI;
public class PartSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject parentImage; // Assign in inspector
    public void SpawnSelectedParts()
    {
        if (RoundStats.partsSelected == true)
        {
            Debug.LogWarning("Parts have already been spawned");
            return;
        }
        CPU cpu = RoundStats.selectedCPU;
        GPU gpu = RoundStats.selectedGPU;
        // Cooler cooler = RoundStats.selectedCooler;
        // RAM ram = RoundStats.selectedRAM;
        // PSU psu = RoundStats.selectedPSU;
        // Storage[] storageArray = RoundStats.selectedStorage;

        // if (cpu == null || gpu == null || cooler == null || ram == null || psu == null || storageArray == null)
        // {
        //     Debug.LogError("One or more selected parts are null. Cannot spawn parts.");
        //     return;
        // }

        SpawnPart(cpu.box, spawnPoints[0]);
        SpawnPart(gpu.box, spawnPoints[1]);
        // SpawnPart(cooler.box, spawnPoints[2]);
        // SpawnPart(ram.box, spawnPoints[3]);
        // SpawnPart(psu.box, spawnPoints[4]);
        // for (int i = 0; i < storageArray.Length; i++)
        // {
        //     if (storageArray[i] != null)
        //     {
        //         SpawnPart(storageArray[i].box, spawnPoints[(5 + i) % spawnPoints.Length]);
        //     }
        // }

        RoundStats.partsSelected = true; // Prevent re-spawning


    }
    public void SpawnPart(GameObject partPrefab, Transform spawnPoint)
    {
        GameObject part = Instantiate(partPrefab, spawnPoint.position, spawnPoint.rotation);
        // Set the parent and ensure local scale/position are correct for UI
        part.transform.SetParent(parentImage.transform, false); // <-- false fixes scaling
    }
}
