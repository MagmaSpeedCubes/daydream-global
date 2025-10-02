using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private BoxCollider2D detectionZone;
    [SerializeField] private BoxCollider2D playerCollider;

    
    public bool IsPlayerInZone()
    {
        return detectionZone.bounds.Intersects(playerCollider.bounds);
    }
}
