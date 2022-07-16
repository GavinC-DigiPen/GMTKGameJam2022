//------------------------------------------------------------------------------
//
// File Name:	EnemyObject.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [Tooltip("The amount of damge the object does")] [SerializeField]
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            GameManger.currentHealth -= damage;
            Destroy(gameObject, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            GameManger.currentHealth -= damage;
            Destroy(gameObject);
        }
    }
}
