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
    [Tooltip("Degrees of the fire")] [SerializeField]
    private float degreesOfFire = 60.0f;

    // Shoot the gun
    protected override void Shoot()
    {
        float startingOffset = degreesOfFire / 2.0f;
        float degreeIncrement = degreesOfFire / (numBullets - 1);
        for (int i = 0; i < numBullets; i++)
        {
            if (gunSound)
            {
                gunAudioSource.PlayOneShot(gunSound);
            }

            GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            float rotation = transform.rotation.eulerAngles.z - startingOffset;
            rotation += degreeIncrement * i;
            newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));

            newBullet.GetComponent<Bullet>().rolledValue = nextBulletValue[i];
            newBullet.GetComponent<Bullet>().Shoot();
            Recoil();
        }
        RollNextDice();
    }
}
