using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Shared Enemy Mechanics
    protected Rigidbody2D rb;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;
    [SerializeField] protected Animator animator;

    public event Action EnemyKilledEvent;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            EnemyKilledEvent?.Invoke();
            animator.SetTrigger("Death");
            Destroy(gameObject);
        }
    }
}
