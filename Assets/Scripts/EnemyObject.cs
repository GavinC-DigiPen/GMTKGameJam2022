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
    [Tooltip("If the object is destroyed on contact")] [SerializeField]
    private bool destroyOnContact = false;
    [Tooltip("If the object destroys on wall")] [SerializeField]
    private bool destroyOnWallContact = false;
    [Tooltip("The particle effect object that is created on destroy")] [SerializeField]
    private GameObject destroyParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            GameManger.currentHealth -= damage;
            if (destroyOnContact)
            {
                SummonParticle();
                Destroy(gameObject);
            }
         }
        if (collision.gameObject.transform.tag == "Wall" && destroyOnWallContact)
        {
            SummonParticle();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            GameManger.currentHealth -= damage;
            if (destroyOnContact)
            {
                SummonParticle();
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.transform.tag == "Wall" && destroyOnWallContact)
        {
            SummonParticle();
            Destroy(gameObject);
        }
    }

    private void SummonParticle()
    {
        if (destroyParticle != null)
        {
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
        }
    }
}
