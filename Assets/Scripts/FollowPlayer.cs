//------------------------------------------------------------------------------
//
// File Name:	FollowPlayer.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Tooltip("The speed of the lerp that moves the object to the target")] [SerializeField]
    private float lerpSpeed = 0.05f;
    [Tooltip("The max distance the object can be from the target before teleporting")] [SerializeField]
    private float maxDistance = 3.0f;

    private GameObject target;

    // Update is called at a fixed rate
    void FixedUpdate()
    {
        if (target == null)
        {
            target = GameManger.player;
            if (target != null)
            {
                ResetPosition();
            }
        }
        else
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Lerp(newPosition.x, target.transform.position.x, lerpSpeed);
            newPosition.y = Mathf.Lerp(newPosition.y, target.transform.position.y, lerpSpeed);
            transform.position = newPosition;

            Vector2 distance = transform.position - target.transform.position;
            if (distance.magnitude > maxDistance)
            {
                ResetPosition();
            }
        }
    }

    // Reset the camera to the player position
    private void ResetPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = target.transform.position.x;
        newPosition.y = target.transform.position.y;
        transform.position = newPosition;
    }
}
