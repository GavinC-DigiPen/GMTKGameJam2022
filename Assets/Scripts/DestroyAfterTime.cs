//------------------------------------------------------------------------------
//
// File Name:	DestoryAfterTime.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Tooltip("The amount of time before object is destroyed")][SerializeField]
    private float time = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }

}
