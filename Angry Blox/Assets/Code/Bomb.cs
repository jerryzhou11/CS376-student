using System;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdImpulse = 5;
    public GameObject ExplosionPrefab;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D point in collision.contacts)
        {
            if (point.normalImpulse > ThresholdImpulse)
            {
                Boom();
            }
        }
    }

    public void Destruct(){
        Destroy(gameObject);
    }

    public void Boom()
    {
        GetComponent<PointEffector2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Invoke("Destruct", 0.1f);
    }
}
