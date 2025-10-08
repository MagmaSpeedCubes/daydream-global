using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "Scriptable Objects/Storage")]
public class Storage : ScriptableObject
{
    public Sprite box;
    public Sprite bg;
    public string name;
    public int capacity; // in GB
    public int readSpeed; // in MB/s
    public int writeSpeed; // in MB/s
    public int price;

    
}
