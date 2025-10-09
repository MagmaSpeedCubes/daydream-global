using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "RAM", menuName = "Scriptable Objects/RAM")]
public class RAM : ScriptableObject
{
    public GameObject box;
    public Sprite bg;
    public string name;
    public int size;
    public int channels;
    public int clockSpeed;
    public int price;
}
