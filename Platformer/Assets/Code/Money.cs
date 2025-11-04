using UnityEngine;

public class Money : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreKeeper.AddToScore(5);
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // destroy if hits ground
            Destroy(gameObject);
        }
    }
}