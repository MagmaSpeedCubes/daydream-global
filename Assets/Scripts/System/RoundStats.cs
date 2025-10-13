using UnityEngine;

public class RoundStats : MonoBehaviour
{
    public static bool partsSelected = false;
    public static string highlightedComponent;
    public static int customerBudget = 2500; //for testing
    public static int systemCost;
    public static CPU selectedCPU;
    public static CPU highlightedCPU;
    public static GPU selectedGPU;
    public static GPU highlightedGPU;
    public static Cooler selectedCooler;
    public static Cooler highlightedCooler;
    public static RAM selectedRAM;
    public static RAM highlightedRAM;

    public static PSU selectedPSU;
    public static PSU highlightedPSU;

    public static Storage[] selectedStorage;
    public static Storage highlightedStorage;


    public static Customer customer;
    public static BuiltPC builtPC;

    public static float buildTime;

    void Awake()
    {
        builtPC = new BuiltPC();
    }
}
