using UnityEngine;

public class FallingIce : MonoBehaviour
{
    private bool hasHitPlayer = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHitPlayer) return; // already hit, ignore
        
        if (other.CompareTag("Player"))
        {
            hasHitPlayer = true;
            ScoreKeeper.AddToScore(-5);
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}