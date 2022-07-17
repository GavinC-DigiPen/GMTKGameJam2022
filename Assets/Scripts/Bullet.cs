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
    [Tooltip("The base damage from the gun")]
    public int baseDamage = -1;
    [Tooltip("The damage value rolled")]
    public int rolledValue = -1;
    [Tooltip("The number of sides the dice has")]
    public int numSides = 6;
    [Tooltip("The sprites for dice")]
    public Sprite[] sprites;
    [Tooltip("The velocity of the bullet")] [SerializeField]
    private float velocity = 15;

    // Shoot the bullet
    public void Shoot()
    {
        Rigidbody2D playerRB = GameManger.player.GetComponent<Rigidbody2D>();

        Vector2 newVelocity = transform.up* velocity;
        if ((newVelocity.x > 0 && playerRB.velocity.x > 0) || (newVelocity.x < 0 && playerRB.velocity.x < 0))
        {
            Debug.Log("test");
            newVelocity.x += playerRB.velocity.x;
        }
        if ((newVelocity.y > 0 && playerRB.velocity.y > 0) || (newVelocity.y < 0 && playerRB.velocity.y < 0))
        {
            newVelocity.y += playerRB.velocity.y;
        }

        GetComponent<Rigidbody2D>().velocity = newVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.currentHealth -= (rolledValue + baseDamage);
            Destroy(gameObject, 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.currentHealth -= (rolledValue + baseDamage);
            Destroy(gameObject);
        }
    }
}
