using UnityEngine;

[CreateAssetMenu(fileName = "CPU", menuName = "Scriptable Objects/CPU")]
public class CPU : ScriptableObject
{
    public GameObject box;
    public Sprite bg;
    public string name;
    public string engraving;
    public int singleCore;
    public int multiCore;
    public int powerConsumption;
    public int price;
    public int integratedGraphics; //0 for none, gpu performance score if present
    

}
