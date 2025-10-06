using UnityEngine;

[CreateAssetMenu(fileName = "Cooler", menuName = "Scriptable Objects/Cooler")]
public class Cooler : ScriptableObject
{
    public Sprite icon;
    public Sprite box;
    public string name;
    public int price;
    public float coolingPower;

}
