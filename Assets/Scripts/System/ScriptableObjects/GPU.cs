using UnityEngine;

[CreateAssetMenu(fileName = "GPU", menuName = "Scriptable Objects/GPU")]
public class GPU : ScriptableObject
{
    public GameObject box;
    public Sprite bg;
    public string name;
    public string engraving;
    public int performance;
    public int VRAM;
    public int powerConsumption;
    public int price;
}
