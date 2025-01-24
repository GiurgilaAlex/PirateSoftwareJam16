using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Mechanism
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool startsClosed;
    
    //To set the bridge's default position. Set start closed in inspector. If you want it to start closed set the z rotation of the bridge to 0. To start open set the z rotation to 90.

    private void Awake()
    {
        if(startsClosed)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0,0,90);
        }
    }
    protected override void Start()
    {
        base.Start();
        animator.SetBool("isOpen", !startsClosed);
    }

    public override void Open()
    {
        base.Open();
        animator.SetBool("isInitialized", true);
        animator.SetBool("isOpen", startsClosed);
    }

    public override void Close()
    {
        base.Close();
        animator.SetBool("isInitialized", true);
        animator.SetBool("isOpen", !startsClosed);
    }
}
