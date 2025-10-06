using UnityEngine;

[CreateAssetMenu(fileName = "BuiltPC", menuName = "Scriptable Objects/BuiltPC")]
public class BuiltPC : ScriptableObject
{
    public CPU cpu;
    public GPU gpu;
    public Cooler cooler;
    public RAM ram;
    public PSU psu;
    public Storage[] storage;
    public int totalPrice;
    public int totalPowerConsumption;
    
}
