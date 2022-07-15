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
    public static GameObject primaryWeapon;
    public static GameObject secondaryWeapon;

    public static int maxHealth = 6;
    public static UnityEvent CurrentHealthUpdate = new UnityEvent();
    private static int _currentHealth = 6;
    public static int currentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            CurrentHealthUpdate.Invoke();
        }
    }
}
