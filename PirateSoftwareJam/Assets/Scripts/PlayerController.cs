using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float speed = 5.0f; //change if needed. test out different values in the inspector.
    [SerializeField] GameObject possessTooltip;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Collider2D objectToPossess;
    private Possessable possessedObject;
    private bool isPossessing = false;
    private Collider2D ownCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        ownCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPossessing)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }


        if (Input.GetKeyDown(KeyCode.Space) && !isPossessing && objectToPossess != null) //Space to possess or something else 
        {
            possessedObject = objectToPossess.gameObject.GetComponent<Possessable>();
            possessedObject.SetIsPossessed(true);
            isPossessing = true;
            sprite.gameObject.SetActive(false);
            ownCollider.enabled = false;
            possessTooltip.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && isPossessing)
        {
            possessedObject.SetIsPossessed(false);
            isPossessing = false;
            possessedObject = null;
            sprite.gameObject.SetActive(true);
            ownCollider.enabled = true;
            possessTooltip.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime * movement.normalized);
        FlipSprite(movement.x);

        if (isPossessing && possessedObject) //lock player to possessed object
        {
            transform.position = possessedObject.transform.position;
        }
    }

    void FlipSprite(float x)
    {
        if (x > 0) { sprite.flipX = false; } //if sprite defaults to facing right else flip these around
        else if (x < 0) { sprite.flipX = true; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Possessable") || other.gameObject.CompareTag("CrystalBall"))
        {
            objectToPossess = other;
            possessTooltip.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Possessable") || other.gameObject.CompareTag("CrystalBall"))
        {
            objectToPossess = null;
            possessTooltip.SetActive(false);
        }
    }
}
