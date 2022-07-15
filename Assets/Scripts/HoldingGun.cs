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
    public Vector3 offSet;
    public float distanceFromCenter;

    // Update is called once per frame
    void Update()
    {
        GameObject heldGun = GameManger.primaryWeapon;
        if (heldGun)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            direction = direction.normalized;

            heldGun.transform.position = (transform.position + offSet) + (direction * distanceFromCenter);
        }
    }
}
