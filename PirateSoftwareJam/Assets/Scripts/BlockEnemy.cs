using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemy : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void Start()
    {
        if (enemy != null)
        {
            enemy.EnemyKilledEvent += HandleEnemyKilled;
        }
    }

    private void OnEnable()
    {
        if (enemy != null)
        {
            enemy.EnemyKilledEvent += HandleEnemyKilled;
        }
    }

    private void OnDisable()
    {
        if (enemy != null)
        {
            enemy.EnemyKilledEvent -= HandleEnemyKilled;
        }
    }

    private void HandleEnemyKilled()
    {
        Destroy(gameObject);
    }
}
