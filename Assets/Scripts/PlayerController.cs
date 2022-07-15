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
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = MostRecentKey(leftKey, rightKey, (int)direction.x);
        direction.y = MostRecentKey(downKey, upKey, (int)direction.y);

        Vector2 newVelocity = playerRB.velocity;
        newVelocity.x += acceleration * direction.x;
        newVelocity.y += acceleration * direction.y;

        if (direction.x == 0)
        {
            newVelocity.x = Mathf.Lerp(newVelocity.x, 0, slowDown);
        }
        if (direction.y == 0)
        {
            newVelocity.y = Mathf.Lerp(newVelocity.y, 0, slowDown);
        }

        playerRB.velocity = newVelocity;
        playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, maxSpeed);
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
