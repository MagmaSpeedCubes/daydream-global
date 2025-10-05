using UnityEngine;

public class GPUDispenser : Dispenser
{
    [SerializeField] private GPU holder;

    override public void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        RoundStats.highlightedGPU = holder;
        RoundStats.highlightedComponent = "GPU";
    }
    override public void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        RoundStats.highlightedGPU = null;
        RoundStats.highlightedComponent = null;
    }
    void Dispense()
    {
        RoundStats.selectedGPU = holder;
    }
}
