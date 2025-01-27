using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possessable : MonoBehaviour
{
    //Shared Object Mechanics
    public bool isPossessed = false;
    public bool isUsed = false;
    protected Rigidbody2D rb;
    protected PlayerController player;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
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

        if(other.gameObject.CompareTag("BlockEnemy"))
        {
            player.UnPossess();
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
