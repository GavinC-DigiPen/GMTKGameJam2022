//------------------------------------------------------------------------------
//
// File Name:	GameManager.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManger : MonoBehaviour
{
    public static UnityEvent PrimaryWeaponUpdate = new UnityEvent();
    private static GameObject _primaryWeapon;
    public static GameObject primaryWeapon
    {
        get
        {
            return _primaryWeapon;
        }
        set
        {
            _primaryWeapon = value;
            PrimaryWeaponUpdate.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
