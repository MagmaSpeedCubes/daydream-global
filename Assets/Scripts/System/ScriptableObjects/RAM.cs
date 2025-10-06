using UnityEngine;

[CreateAssetMenu(fileName = "RAM", menuName = "Scriptable Objects/RAM")]
public class RAM : ScriptableObject
{
    public Sprite bg;
    public int size;
    public int channels;
    public int clockSpeed;
    public int price;
}
