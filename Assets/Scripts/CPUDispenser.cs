using UnityEngine;

public class CPUDispenser : Dispenser
{
    [SerializeField] private CPU holder;

    override public void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        RoundStats.highlightedCPU = holder;
        RoundStats.highlightedComponent = "CPU";
    }
    override public void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        RoundStats.highlightedCPU = null;
        RoundStats.highlightedComponent = null;
    }
    void Dispense()
    {
        RoundStats.selectedCPU = holder;
    }
}
