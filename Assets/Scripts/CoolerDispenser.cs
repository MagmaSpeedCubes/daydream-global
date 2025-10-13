using UnityEngine;

public class CoolerDispenser : Dispenser
{
    [SerializeField] private Cooler holder;

    override public void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        RoundStats.highlightedCooler = holder;
        RoundStats.highlightedComponent = "Cooler";
    }
    override public void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        RoundStats.highlightedCooler = null;
        RoundStats.highlightedComponent = null;
    }
    void Dispense()
    {
        RoundStats.selectedCooler = holder;
    }
}
