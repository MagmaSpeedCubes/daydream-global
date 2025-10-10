using UnityEngine;

[CreateAssetMenu(fileName = "PSU", menuName = "Scriptable Objects/PSU")]
public class PSU : ScriptableObject
{
    public GameObject box;
    public Sprite bg;
    public string name;
    public int power;
    public float efficiency;
    public int price;
}
