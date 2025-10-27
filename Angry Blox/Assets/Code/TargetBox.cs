using NUnit.Framework.Constraints;
using UnityEngine;

public class TargetBox : MonoBehaviour
{
    /// <summary>
    /// Targets that move past this point score automatically.
    /// </summary>
    public static float OffScreen;
    private bool scoredOnce = false;

    internal void Start() {
        OffScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-100, 0, 0)).x;
    }

    internal void Update()
    {
        if (transform.position.x > OffScreen && !this.scoredOnce)
        {
            Scored();
            this.scoredOnce = true;
        }
    }

    private void Scored()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        ScoreKeeper.AddToScore(GetComponent<Rigidbody2D>().mass);
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && !this.scoredOnce)
        {
            Scored();
            this.scoredOnce = true;
        }

    }
}
