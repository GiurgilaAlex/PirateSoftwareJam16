using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LanceProjectile : MonoBehaviour
{
    [SerializeField] private Collider2D coll;
    [SerializeField] private Collider2D coll1;
    [SerializeField] private float destroyTimer;
    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.CompareTag("Wall") || collision.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            coll.enabled = false;
            coll1.enabled = false;
        }*/

        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnHit(1);
            StartCoroutine(TimedDestroy(0.1f));
        }

        else if(collision.CompareTag("CrystalBall"))
        {
            //collision.GetComponent<CrystalBall>().OnHit();
        }

        else
        {

            StartCoroutine(TimedDestroy(destroyTimer));
        }
    }

    IEnumerator TimedDestroy(float _destroyTimer)
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
        coll.enabled = false;
        coll1.enabled = false;
        yield return new WaitForSeconds(_destroyTimer);
        Destroy(gameObject);
    }
    
}
