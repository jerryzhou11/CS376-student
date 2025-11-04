using UnityEngine;

public class MovingIce : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 4f;
    private float moveDirection;
    private bool hasHitPlayer = false;

    
    void Start()
    {
        // direction will be set by spawner, but default to right
        moveDirection = 1f;
    }
    
    public void SetDirection(float direction)
    {
        moveDirection = direction;
    }
    
    void Update()
    {
        // move horizontally
        transform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);
    }
    
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHitPlayer) return; // already hit, ignore
        if (other.CompareTag("Player"))
        {
            hasHitPlayer = true;
            ScoreKeeper.AddToScore(-5);
            Destroy(gameObject);
        }
    }
}