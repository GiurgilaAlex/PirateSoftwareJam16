using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Possessable
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private PlayerController player;
    [SerializeField] private Collider2D triggerCollider;

    private float xInput;
    private float rotationValue = 0f;
    private bool isThrownInTheAir = false;
    private bool isGoingRight;

    private void Update()
    {
        if(isPossessed)
        {
            xInput = Input.GetAxisRaw("Horizontal");
        
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                Vector2 direction = (projectileSpawnPoint.position - transform.position).normalized;
                if(direction.x > 0f)
                {
                    isGoingRight = true;
                    transform.localScale = new Vector3(1, 1, 1);
                } else
                {
                    isGoingRight = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                rb.AddForce(direction * speed, ForceMode2D.Impulse);
                isThrownInTheAir = true;
                shootingPoint.SetActive(false);
                StartCoroutine(CheckCollisionAfterAWHile());
            }

            if (isThrownInTheAir)
            {
                if (isGoingRight)
                {
                    transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPossessed)
        {
            rotationValue = rotationValue + xInput * arrowSpeed * Time.fixedDeltaTime;
            shootingPoint.transform.rotation = Quaternion.Euler(0, 0, -rotationValue);
        }
    }

    private IEnumerator CheckCollisionAfterAWHile()
    {
        //We check this because if the axe is already colliding with the ground
        //and the player pushes it into the ground, then OnTriggerEnter2D won't get called

        yield return new WaitForSeconds(0.2f);

        Collider2D[] results = new Collider2D[2];
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();

        int count = triggerCollider.OverlapCollider(filter, results);

        if(count > 0)
        {
            for(int i = 0; i < count; i++)
            {
                if (results[i].CompareTag("Wall") || results[i].CompareTag("Ground"))
                {
                    rb.bodyType = RigidbodyType2D.Static;
                    isThrownInTheAir = false;

                    if(isPossessed)
                    {
                        shootingPoint.SetActive(true);
                    }
                }
            }
        }
    }

    public override void SetIsPossessed(bool value)
    {
        base.SetIsPossessed(value);

        if (isPossessed)
        {
            shootingPoint.SetActive(true);
        }
        else
        {
            shootingPoint.transform.rotation = Quaternion.identity;
            rotationValue = 0f;
            shootingPoint.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.CompareTag("Wall") || collision.CompareTag("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            isThrownInTheAir = false;
            if(isPossessed)
            {
                shootingPoint.SetActive(true);
            }
        }

        if (collision.CompareTag("Enemy") && isPossessed && isThrownInTheAir)
        {
            collision.GetComponent<Enemy>().OnHit(1);
        }

        if (collision.CompareTag("CrystalBall") && isPossessed && isThrownInTheAir)
        {
            collision.GetComponent<CrystalBall>().OnHit();
        }
    }
}
