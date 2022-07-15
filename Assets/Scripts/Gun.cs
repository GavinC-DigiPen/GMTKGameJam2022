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
    [Tooltip("If the gun is being held or not")] [SerializeField] 
    public bool isHeld = false;
    [Tooltip("The prefab of the bullet")] [SerializeField] 
    private GameObject bulletPrefab;
    [Tooltip("The time between bullet shots")] [SerializeField]
    private float shotDelay = 0.5f;

    private float shotTimer = 0;

    // Update is called once per frame
    void Update()
    {
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
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.rotation = transform.rotation;
    }

    // Set the value of the dice
    void RollDice()
    {
        
    }
}
