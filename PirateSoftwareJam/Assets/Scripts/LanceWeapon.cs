using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceWeapon : Possessable
{
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float projectileSpeed;

    private float xInput;
    private float rotationValue = 0f;

    private void Update()
    {
        if(isPossessed)
        {
            xInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector2 direction = (projectileSpawnPoint.position - transform.position).normalized;
                var projectileInstance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity, null);
                projectileInstance.transform.rotation = Quaternion.Euler(0, 0, -rotationValue);
                projectileInstance.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        if(isPossessed)
        {
            rotationValue = Mathf.Clamp(rotationValue + xInput * arrowSpeed * Time.fixedDeltaTime, -90, 90);
            shootingPoint.transform.rotation = Quaternion.Euler(0, 0, -rotationValue);
        }
    }

    public override void SetIsPossessed(bool value)
    {
        base.SetIsPossessed(value);

        if (isPossessed)
        {
            shootingPoint.SetActive(true);
        } else
        {
            shootingPoint.transform.rotation = Quaternion.identity;
            rotationValue = 0f;
            shootingPoint.SetActive(false);
        }
    }
}
