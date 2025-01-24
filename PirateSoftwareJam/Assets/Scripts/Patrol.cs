using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed;

    private Transform currentPoint;
    private Rigidbody2D rb;
    private Enemy enemy;
    private bool isEnemyKilled = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        currentPoint = pointB.transform;

        if (enemy != null)
        {
            enemy.EnemyKilledEvent += HandleEnemyKilled;
        }
    }

    private void OnEnable()
    {
        if(enemy != null)
        {
            enemy.EnemyKilledEvent += HandleEnemyKilled;
        }
    }

    private void OnDisable()
    {
        if(enemy != null)
        {
            enemy.EnemyKilledEvent -= HandleEnemyKilled;
        }
    }

    private void Update()
    {
        if(!isEnemyKilled)
        {
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                Flip();
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                Flip();
                currentPoint = pointB.transform;
            }
        }
    }

    private void HandleEnemyKilled()
    {
        isEnemyKilled = true;
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
