using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bucket : Possessable
{
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    protected override void Start()
    {
        base.Start();
        animator = transform.GetChild(0).GetComponent<Animator>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isPossessed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Tip");
                boxCollider2D.offset = new Vector2(0,0.15f);
                boxCollider2D.size = new Vector2(1,0.75f);
                gameObject.layer = LayerMask.NameToLayer("Tip Bucket");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isUsed && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().OnHit(1);
            isUsed = true;
        }

        if (other.gameObject.CompareTag("CrystalBall"))
        {
            //other.gameObject.GetComponent<CrystalBall>().OnHit();
            //isUsed = true;
        }
    }
}
