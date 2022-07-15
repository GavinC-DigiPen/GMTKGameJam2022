//------------------------------------------------------------------------------
//
// File Name:	Shotgun.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------`

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [Tooltip("The number of bullets to spawn")] [SerializeField] 
    private int numBullets = 3;
    [Tooltip("Degrees of the fire")] [SerializeField]
    private float degreesOfFire = 60.0f;

    // Set the value of the dice
    protected override void RollNextDice()
    {
        for (int i = 0; i < numBullets; i++)
        {
            nextBulletValue[i] = Random.Range(1, bulletPrefab.GetComponent<Bullet>().numSides + 1);
        }
    }

    // Shot the gun
    protected override void Shoot()
    {
        float startingOffset = degreesOfFire / 2.0f;
        float degreeIncrement = degreesOfFire / (numBullets - 1);
        for (int i = 0; i < numBullets; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            float rotation = transform.rotation.eulerAngles.z - startingOffset;
            rotation += degreeIncrement * i;
            newBullet.transform.rotation = Quaternion.Euler(new Vector3(newBullet.transform.rotation.x, newBullet.transform.rotation.y, rotation));

            newBullet.GetComponent<Bullet>().rolledValue = nextBulletValue[i];
            newBullet.GetComponent<Bullet>().Shoot();
        }
        RollNextDice();
    }
}
