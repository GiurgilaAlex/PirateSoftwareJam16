using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceProjectile : MonoBehaviour
{
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
        }
    }
}
