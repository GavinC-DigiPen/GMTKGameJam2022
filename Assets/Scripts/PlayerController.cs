using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Scalar for player max speed
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        // get user input
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        // create a 2D translation based on user input
        Vector3 translation = new Vector3(deltaX, deltaY, 0);

        // transform the player based on user input and scaled by the max speed of the player
        transform.Translate(translation * speed);
    }
}
