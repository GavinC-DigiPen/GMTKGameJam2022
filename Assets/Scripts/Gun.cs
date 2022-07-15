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
    private GameObject bulletPrefab;
    [Tooltip("The time between bullet shots")] [SerializeField]
    private float shotDelay = 0.5f;

    private float shotTimer = 0;
    private int nextBulletValue = -1;
    private SpriteRenderer nextBulletIndicator;

    // Start is called before the first frame update
    void Start()
    {
        //testing
        GameManger.primaryWeapon = gameObject;

        nextBulletIndicator = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
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
        }

        // Shooting gun
        if (isHeld && shotTimer <= 0 &&  Input.GetMouseButton(0))
        {
            Shoot();
            shotTimer = shotDelay;
        }
        if (shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
        }
    }

    // Shot the gun
    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        newBullet.GetComponent<Bullet>().rolledValue = nextBulletValue;
        newBullet.GetComponent<Bullet>().Shoot();
        RollNextDice();
    }

    // Set the value of the dice
    void RollNextDice()
    {
        nextBulletValue = Random.Range(1, bulletPrefab.GetComponent<Bullet>().numSides + 1);
        nextBulletIndicator.sprite = bulletPrefab.GetComponent<Bullet>().sprites[nextBulletValue - 1];
    }
}
