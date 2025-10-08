using UnityEngine;

public class StorageDispenser : Dispenser
{
    [SerializeField] private Storage holder;

    override public void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        RoundStats.highlightedStorage = holder;
        RoundStats.highlightedComponent = "Storage";
    }
    override public void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        RoundStats.highlightedStorage = null;
        RoundStats.highlightedComponent = null;
    }
    void Dispense()
    {
        System.Array.Resize(ref RoundStats.selectedStorage, RoundStats.selectedStorage.Length + 1);
        RoundStats.selectedStorage[RoundStats.selectedStorage.Length - 1] = holder;

    }

    void Remove()
    {
        int index = System.Array.IndexOf(RoundStats.selectedStorage, holder);
        if (index >= 0)
        {
            for (int i = index; i < RoundStats.selectedStorage.Length - 1; i++)
            {
                RoundStats.selectedStorage[i] = RoundStats.selectedStorage[i + 1];
            }
            System.Array.Resize(ref RoundStats.selectedStorage, RoundStats.selectedStorage.Length - 1);
        }
    }
}
