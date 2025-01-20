using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possessable : MonoBehaviour
{
    //Shared Object Mechanics
    protected bool isPossessed = false;
    public bool isUsed = false;
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public virtual void Attack()
    {

    }

    public virtual void SetIsPossessed(bool value)
    {
        isPossessed = value;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //outline on
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //outline off
        }
    }

}
