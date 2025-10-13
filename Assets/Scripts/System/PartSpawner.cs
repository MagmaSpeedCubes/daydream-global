using UnityEngine;
using UnityEngine.UI;
public class PartSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject parentImage; // Assign in inspector
    [SerializeField] private GameObject parent2Image;
    [SerializeField] protected GameObject inspireMotherboard, hcbMotherboard, moboSpawnPoint, moboSpawnLayer;
    [SerializeField] protected GameObject inspireSockets, hcbSockets, socketSpawnPoint, socketSpawnLayer;

    public void SpawnSelectedParts()
    {
        if (RoundStats.partsSelected == true)
        {
            Debug.LogWarning("Parts have already been spawned");
            return;
        }
        CPU cpu = RoundStats.selectedCPU;

        string cpuBrand = cpu.name.Substring(0, cpu.name.IndexOf(" "));
        if (cpuBrand == "Inspire")
        {
            SpawnPart(inspireMotherboard, moboSpawnPoint.transform, moboSpawnLayer);
            SpawnPart(inspireSockets, socketSpawnPoint.transform, socketSpawnLayer);
            Debug.Log("Spawned mobos");
        }
        else if (cpuBrand == "HCB")
        {
            SpawnPart(hcbMotherboard, moboSpawnPoint.transform, moboSpawnLayer);
            SpawnPart(hcbSockets, socketSpawnPoint.transform, socketSpawnLayer);
            Debug.Log("Spawned mobos");
        }



        GPU gpu = RoundStats.selectedGPU;
        Cooler cooler = RoundStats.selectedCooler;
        RAM ram = RoundStats.selectedRAM;
        // PSU psu = RoundStats.selectedPSU;
        Storage[] storageArray = RoundStats.selectedStorage;

        if (cpu == null || gpu == null || cooler == null || ram == null || storageArray == null)
        {
            Debug.LogError("One or more selected parts are null. Cannot spawn parts.");
            return;
        }

        SpawnPart(cpu.box, spawnPoints[0], parentImage);
        SpawnPart(gpu.box, spawnPoints[1], parent2Image);
        SpawnPart(cooler.box, spawnPoints[2], parent2Image);
        SpawnPart(ram.box, spawnPoints[3], parent2Image);
        // SpawnPart(psu.box, spawnPoints[4]);
        for (int i = 0; i < storageArray.Length; i++)
        {
            if (storageArray[i] != null)
            {

                SpawnPart(storageArray[i].box, spawnPoints[(5 + i) % spawnPoints.Length], parentImage);
            }
        }

        RoundStats.partsSelected = true; // Prevent re-spawning


    }
    public void SpawnPart(GameObject partPrefab, Transform spawnPoint, GameObject parentImage)
    {
        GameObject part = Instantiate(partPrefab, spawnPoint.position, spawnPoint.rotation);
        // Set the parent and ensure local scale/position are correct for UI
        part.transform.SetParent(parentImage.transform, false); // <-- false fixes scaling
        Debug.Log("Spawned new part");
    }

}
