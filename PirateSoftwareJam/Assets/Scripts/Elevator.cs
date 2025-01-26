using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Mechanism
{
    [SerializeField] private GameObject aTerminal;
    [SerializeField] private GameObject bTerminal;
    [SerializeField] private GameObject line;
    [SerializeField] private bool bToA;
    [SerializeField] private float speed;
    [SerializeField] private GameObject doors;
    private Vector2 start;
    private Vector2 end;
    private Vector2 delta;
    private float direction;
    private float stopTolerance = 0.1f;


    protected override void Start()
    {
        base.Start();
        if (bToA)
        {
            start = bTerminal.transform.position;
            end = aTerminal.transform.position;
        }
        else
        {
            start = aTerminal.transform.position;
            end = bTerminal.transform.position;
        }
        rb.position = start;
        delta = end - start;        

    }

    private void FixedUpdate()
    {
        //open should set direction to 1 and the elevator should start moving to end and then set direction to 0. close should set direction to -1 and go back to start and set direction to 0.
        if(direction == 1f & Vector2.Distance(rb.position, end) >= stopTolerance)
        {
            rb.MovePosition(rb.position + delta * direction * speed * Time.deltaTime);
        }

        if(direction == -1f & Vector2.Distance(rb.position, start) >= stopTolerance)
        {
            rb.MovePosition(rb.position + delta * direction * speed * Time.deltaTime);
        }
    }

    public override void Open()
    {
        base.Open();
        direction = 1f;
    }

    public override void Close()
    {
        base.Close();
        direction = -1f;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if((other.gameObject.CompareTag("CrystalBall") | other.gameObject.CompareTag("Possessable")) && !other.gameObject.GetComponent<Possessable>().isPossessed)
        {
            other.transform.SetParent(gameObject.transform);

            Rigidbody2D otherRb = other.gameObject.GetComponent<Rigidbody2D>();
            otherRb.isKinematic = true;
            otherRb.velocity = rb.velocity;
            otherRb.angularVelocity = rb.angularVelocity;
        }
        
        //Added this so the isKinematic won't stay true when the object has been possessed while inside the elevator
        if(other.gameObject.GetComponent<Possessable>().isPossessed)
        {
            other.transform.SetParent(null);
            other.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("CrystalBall") | other.gameObject.CompareTag("Possessable"))
        {
            other.transform.SetParent(null);
            other.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
