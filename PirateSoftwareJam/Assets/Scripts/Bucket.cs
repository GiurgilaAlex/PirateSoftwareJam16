using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Possessable
{
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = transform.GetChild(0).GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        if (isPossessed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Tip");

                gameObject.layer = LayerMask.NameToLayer("Tip Bucket");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().OnHit(1);
            isUsed = true;
        }

        if (other.gameObject.CompareTag("CrystalBall"))
        {
            other.gameObject.GetComponent<CrystalBall>().OnHit();
            isUsed = true;
        }
    }
}
