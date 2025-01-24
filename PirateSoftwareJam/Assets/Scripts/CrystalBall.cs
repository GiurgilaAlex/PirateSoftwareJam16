using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBall : Possessable
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject enemyDetection;
    [SerializeField] private Animator anim;
    
    private float xInput;
    private float amp = 0.2f, freq = 3f;
    private Vector2 initPos;
    private float floatOffset = 0.5f;
    private bool isDestroyed = false;

    protected override void Start()
    {
        base.Start();
    }

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
            float posY = Mathf.Sin(Time.fixedTime * freq) * amp + initPos.y;

            if (isDestroyed)
            {
                posY = transform.position.y;
            }

            //rb.position = new Vector2(rb.position.x + speed * Time.fixedDeltaTime * xInput, Mathf.Sin(Time.fixedTime * freq) * amp + initPos.y);
            rb.MovePosition(new Vector2(rb.position.x + speed * Time.fixedDeltaTime * xInput, Mathf.Sin(Time.fixedTime * freq) * amp + posY));
        }
    }

    public override void SetIsPossessed(bool isPossessed)
    {
        base.SetIsPossessed(isPossessed);

        if(isPossessed)
        {
            enemyDetection.SetActive(true);
            transform.position += new Vector3(0, floatOffset, 0);
            initPos = transform.position;
            //rb.isKinematic = true;
            rb.gravityScale = 0f;
        } else
        {
            enemyDetection.SetActive(false);
            //rb.isKinematic = false;
            rb.gravityScale = 1f;
        }
    }

    public void OnHit()
    {
        isDestroyed = true;
        anim.SetTrigger("Death");
        StartCoroutine(WaitAndResetLevel());
    }

    private IEnumerator WaitAndResetLevel()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.ObjectToDefendKilled();
    }
}
