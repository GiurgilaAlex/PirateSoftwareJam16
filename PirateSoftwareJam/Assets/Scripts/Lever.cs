using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] Mechanism mechanism;
    [SerializeField] Animator leverAnimator;
    [SerializeField] GameObject closedCollider;
    [SerializeField] GameObject openCollider;
    private BoxCollider2D boxCollider2D;
    public bool isOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            //set mechanism to open
            leverAnimator.SetBool("isOpen", isOpen);
            StartCoroutine(FlipLever(isOpen));
            mechanism.Open();
        }
        else
        {   
            //set mechanism to closed
            leverAnimator.SetBool("isOpen", isOpen);
            StartCoroutine(FlipLever(isOpen));
            mechanism.Close();
        }
    }

    IEnumerator FlipLever(bool _isOpen)
    {
        boxCollider2D.offset = new Vector2(-boxCollider2D.offset.x,boxCollider2D.offset.y);
        yield return new WaitForSeconds(1);
        openCollider.SetActive(_isOpen);
        closedCollider.SetActive(!_isOpen);
    }
}
