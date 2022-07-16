//------------------------------------------------------------------------------
//
// File Name:	HoldingGun.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingGun : MonoBehaviour
{
    [Tooltip("The offset of the center")]
    public Vector3 offset;
    [Tooltip("The distance from center the gun will be")]
    public float distanceFromCenter;

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject heldGun = GameManger.primaryWeapon;
        if (heldGun)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            direction = direction.normalized;

            heldGun.transform.position = (transform.position + offset) + (direction * distanceFromCenter);
        }
    }
}
