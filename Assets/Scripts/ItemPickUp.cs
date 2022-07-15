//------------------------------------------------------------------------------
//
// File Name:	ItemPickUp.cs
// Author(s):	Dominic
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    public KeyCode key;
    private List<GameObject> touchingGuns;

    void Start() 
    {
        touchingGuns = new List<GameObject> ();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject GO = col.gameObject;

        bool isGun = GO.GetComponent<Gun>() != null;

        if (isGun) 
        {
            touchingGuns.Add(GO);
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        GameObject GO = col.gameObject;

        bool isGun = GO.GetComponent<Gun>() != null;

        if (isGun)
        {
            touchingGuns.Remove(GO);
        }
    }

    void Update()
    {
        bool keyPressed = Input.GetKeyDown(key);

        if (keyPressed) 
        {
            GameObject closestDroppedGun = null;
            float min_distance = 100;

            for (int i = 0; i < touchingGuns.Count; i++) 
            {
                GameObject gun = touchingGuns[i];

                if (!gun.GetComponent<Gun>().isHeld)
                {
                    float new_distance = (gun.transform.position - gameObject.transform.position).magnitude;

                    if (new_distance < min_distance)
                    {
                        min_distance = new_distance;

                        closestDroppedGun = gun;
                    }
                }
            }

            if (closestDroppedGun != null) {
                if (GameManger.primaryWeapon != null)
                {
                    GameManger.primaryWeapon.GetComponent<Gun>().isHeld = false;
                }

                GameManger.primaryWeapon = closestDroppedGun;
                GameManger.primaryWeapon.GetComponent<Gun>().isHeld = true;
            }
        }
    }
}
