//------------------------------------------------------------------------------
//
// File Name:	Bullet.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("The number of sides the dice has")]
    public int numSides = 6;
    [Tooltip("The velocity of the bullet")] [SerializeField]
    private float velocity = 15;
    [Tooltip("The sprites for dice")]
    public Sprite[] sprites;
    [Tooltip("The particle effect object that is created on hit")] [SerializeField]
    private GameObject hitParticle;
    [Tooltip("The base damage from the gun")]
    public int baseDamage = -1;
    [Tooltip("The damage value rolled")]
    public int rolledValue = -1;
    [Tooltip("Velocity to add to bullets from player (Script)")]
    public Vector2 playerVelocityToAdd;

    // Shoot the bullet
    public void Shoot()
    {
        Vector2 newVelocity = ((Vector2)transform.up * velocity) + playerVelocityToAdd;

        GetComponent<Rigidbody2D>().velocity = newVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.currentHealth -= (rolledValue + baseDamage);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.transform.tag == "Wall")
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.currentHealth -= (rolledValue + baseDamage);
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.transform.tag == "Wall")
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
