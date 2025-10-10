using UnityEngine;

[CreateAssetMenu(fileName = "Cooler", menuName = "Scriptable Objects/Cooler")]
public class Cooler : ScriptableObject

{
    public GameObject box;
    public Sprite icon;
    public string name;
    public int price;
    public float coolingPower;

}
