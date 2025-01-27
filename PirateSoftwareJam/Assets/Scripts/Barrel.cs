using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Barrel : Possessable
{
    private float direction = 0; //-1 == roll left, 0 == no direction set, 1 == roll right
    private float input;
    [SerializeField] private float pushForce;
    [SerializeField] private GameObject barrelStanding;
    [SerializeField] private GameObject barrelRolling;
    [SerializeField] private GameObject rollDir;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {   if (isPossessed)
        {
            input  = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(input) == 1)
            {
                direction = input;
                rollDir.transform.rotation = Quaternion.Euler(0,180,90 * direction);
            }

            if (direction != 0 && Input.GetKeyDown(KeyCode.E))
            {
                rollDir.transform.rotation = Quaternion.Euler(90,180,0);//hide after rolling.
                barrelStanding.SetActive(false);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                barrelRolling.SetActive(true);
                rb.AddForce(Vector2.right * pushForce * direction, ForceMode2D.Impulse);

            }
        }
    }

    public override void SetIsPossessed(bool value)
    {
        base.SetIsPossessed(value);

        if (isPossessed)
        {
            rollDir.SetActive(true);
        } else
        {
            rollDir.transform.rotation = Quaternion.Euler(90,180,0);
            rollDir.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().OnHit(1);
            player.UnPossess();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("CrystalBall"))
        {
            //other.gameObject.GetComponent<CrystalBall>().OnHit();
            //Destroy(gameObject);
        }
    }

}
