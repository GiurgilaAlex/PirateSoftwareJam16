using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBall : Possessable
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject enemyDetection;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayerMask;
    
    private float xInput;
    private float floatOffset = 0.5f;

    private void Update()
    {
        if(isPossessed)
        {
            xInput = Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        if(isPossessed)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, groundLayerMask);

            if (hit.collider != null)
            {
                rb.AddForce(transform.up * (2 / (hit.distance / 2)));
            }

            //This needs to be set like this so it won't mess up the gravity
            Vector2 newVelocity = new Vector2(xInput * speed, rb.velocity.y);
            rb.velocity = newVelocity;
        }
    }

    public override void SetIsPossessed(bool isPossessed)
    {
        base.SetIsPossessed(isPossessed);

        if(isPossessed)
        {
            Vector3 pos = transform.position;
            pos.y -= 0.5f;
            transform.position = pos;
            anim.SetTrigger("PickUp");
            enemyDetection.SetActive(true);
            transform.position += new Vector3(0, floatOffset, 0);
        } else
        {
            anim.SetTrigger("Drop");
            enemyDetection.SetActive(false);
        }
    }

    public void OnHit()
    {
        anim.SetTrigger("Death");
        StartCoroutine(WaitAndResetLevel());
    }

    private IEnumerator WaitAndResetLevel()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.ObjectToDefendKilled();
    }
}
