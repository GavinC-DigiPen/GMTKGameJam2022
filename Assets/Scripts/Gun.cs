//------------------------------------------------------------------------------
//
// File Name:	Gun.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Tooltip("If the gun is being held or not")]
    public bool isHeld = false;
    [Tooltip("The prefab of the bullet")] [SerializeField] 
    protected GameObject bulletPrefab;
    [Tooltip("The time between bullet shots")] [SerializeField]
    private float shotCooldown = 0.5f;

    private float shotTimer = 0.0f;
    protected int[] nextBulletValue = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

    // Start is called before the first frame update
    void Start()
    {
        RollNextDice();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotating gun
        if (isHeld)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            direction = direction.normalized;
            float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rotation));

            if (-rotation > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        // Shooting gun
        if (isHeld && shotTimer <= 0 &&  Input.GetMouseButton(0))
        {
            Shoot();
            shotTimer = shotCooldown;
        }
        if (shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
        }
    }

    // Set the value of the dice
    virtual protected void RollNextDice()
    {
        nextBulletValue[0] = Random.Range(1, bulletPrefab.GetComponent<Bullet>().numSides + 1);
    }

    // Shoot the gun
    virtual protected void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        newBullet.GetComponent<Bullet>().rolledValue = nextBulletValue[0];
        newBullet.GetComponent<Bullet>().Shoot();
        RollNextDice();
    }
}
