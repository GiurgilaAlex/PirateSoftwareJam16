using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Possessable
{
    private float direction = 0; //-1 == roll left, 0 == no direction set, 1 == roll right
    private float input;
    [SerializeField] private float pushForce;
    [SerializeField] private GameObject barrelStanding;
    [SerializeField] private GameObject barrelRolling;
    [SerializeField] private GameObject rollDir;
    [SerializeField] private GameObject player;

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
                barrelStanding.SetActive(false);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                barrelRolling.SetActive(true);
                rb.AddForce(Vector2.right * pushForce * direction, ForceMode2D.Impulse);
                player.GetComponent<PlayerController>().UnPossess();
                isUsed = true;

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

}
