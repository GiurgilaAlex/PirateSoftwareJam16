using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceProjectile : MonoBehaviour
{
    [SerializeField] private Collider2D coll;
    [SerializeField] private Collider2D coll1;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            coll.enabled = false;
            coll1.enabled = false;
        }

        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnHit(1);
        }

        if(collision.CompareTag("CrystalBall"))
        {
            collision.GetComponent<CrystalBall>().OnHit();
        }
    }
}
