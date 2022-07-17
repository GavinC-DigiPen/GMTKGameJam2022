//------------------------------------------------------------------------------
//
// File Name:	PlayerController.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("The maximum speed")]
    public float maxSpeed = 5f;
    [Tooltip("The acceleration")]
    public float acceleration = 0.5f;
    [Tooltip("How fast the player slows down when not moving (0 - 1)")]
    public float slowDown = 0.01f;

    private Rigidbody2D playerRB;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;

    private Vector2 direction = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        GameManger.player = gameObject;
        GameManger.currentHealth = GameManger.maxHealth;

        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called at fixed rate
    void FixedUpdate()
    {
        // Get direction
        direction.x = MostRecentKey(leftKey, rightKey, (int)direction.x);
        direction.y = MostRecentKey(downKey, upKey, (int)direction.y);

        // Accelerate
        Vector2 newVelocity = playerRB.velocity;
        newVelocity.x += acceleration * direction.x;
        newVelocity.y += acceleration * direction.y;

        // Slow player down
        if (direction.x == 0)
        {
            newVelocity.x = Mathf.Lerp(newVelocity.x, 0, slowDown);
        }
        if (direction.y == 0)
        {
            newVelocity.y = Mathf.Lerp(newVelocity.y, 0, slowDown);
        }

        // Change Velocity
        playerRB.velocity = newVelocity;
        playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, maxSpeed);

        // Flip player with gun
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionVector = mousePosition - transform.position;
        directionVector = directionVector.normalized;
        float rotation = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        GetComponent<SpriteRenderer>().flipX = (rotation < 0);
    }

    // Find the most recent key pushed or currently pushed
    // Parms:
    //  negative: the key that will result in a -1 returned
    //  positive: the key that will result in a 1 returned
    //  lastDirection: the last direction moved
    // Returns:
    //  -1 or 1 based on which button is currently active, 0 if no key
    private int MostRecentKey(KeyCode negative, KeyCode positive, int lastDirection)
    {
        int direction = lastDirection;
        if (Input.GetKeyDown(negative))
        {
            direction = -1;
        }
        else if (Input.GetKey(negative) && !Input.GetKey(positive))
        {
            direction = -1;
        }
        else if (Input.GetKeyDown(positive))
        {
            direction = 1;
        }
        else if (Input.GetKey(positive) && !Input.GetKey(negative))
        {
            direction = 1;
        }
        else if (!Input.GetKey(negative) && !Input.GetKey(positive))
        {
            direction = 0;
        }

        return direction;
    }
}
