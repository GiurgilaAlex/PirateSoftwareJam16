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
                gameObject.tag = "Untagged";
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
                player.UnPossess();
                isThrownInTheAir = true;
                StartCoroutine(DisableCollisionForABit());
            }
        } else
        {
            if(isThrownInTheAir)
            {
                if(isGoingRight)
                {
                    transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
                } else
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

    private IEnumerator DisableCollisionForABit()
    {
        //Doing this so the axe doesn't slide on the ground if you throw it while is already colliding with it
        triggerCollider.enabled = false;
        yield return new WaitForSeconds(0.2f);
        triggerCollider.enabled = true;
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
            gameObject.tag = "Possessable";
            rb.bodyType = RigidbodyType2D.Static;
            isThrownInTheAir = false;
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnHit(1);
        }

        if (collision.CompareTag("CrystalBall"))
        {
            collision.GetComponent<CrystalBall>().OnHit();
        }
    }
}
