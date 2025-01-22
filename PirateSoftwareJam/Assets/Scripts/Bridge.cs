using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Mechanism
{
    [SerializeField] private Animator animator;

    public override void Open()
    {
        base.Open();
        animator.SetBool("isOpen", true);
    }

    public override void Close()
    {
        base.Close();
        animator.SetBool("isOpen", false);
    }
}
