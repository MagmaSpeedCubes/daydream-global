using UnityEngine;

public class RAMDispenser : Dispenser
{
    [SerializeField] private RAM holder;

    override public void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        RoundStats.highlightedRAM = holder;
        RoundStats.highlightedComponent = "RAM";
    }
    override public void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        RoundStats.highlightedRAM = null;
        RoundStats.highlightedComponent = null;
    }
    void Dispense()
    {
        RoundStats.selectedRAM = holder;
    }
}
