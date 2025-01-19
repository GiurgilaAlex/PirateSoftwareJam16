using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBall : Possessable
{
    [SerializeField]
    private float speed = 2f;

    private float xMovement;
    private float amp = 0.2f, freq = 3f;
    private Vector2 initPos;
    private float floatOffset = 0.5f;

    private void Update()
    {
        if(isPossessed)
        {
            xMovement = Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        if(isPossessed)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.fixedDeltaTime * xMovement, Mathf.Sin(Time.fixedTime * freq) * amp + initPos.y);
        }
    }

    public override void SetIsPossessed(bool isPossessed)
    {
        base.SetIsPossessed(isPossessed);

        if(isPossessed)
        {
            transform.position += new Vector3(0, floatOffset, 0);
            initPos = transform.position;
            rb.isKinematic = true;
        } else
        {
            rb.isKinematic = false;
        }
    }
}
