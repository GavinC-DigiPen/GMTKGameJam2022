//------------------------------------------------------------------------------
//
// File Name:	CameraFollowPlayer.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Tooltip("The speed of the lerp that moves the object to the target")] [SerializeField]
    private float lerpSpeed = 0.05f;
    [Tooltip("The persent of distance from mouse the target location will be moved towards the mouse")] [SerializeField]
    private float persentOfDistanceFromMouse = 0.0f;
    [Tooltip("The mimimum distance from the target the mouse must be to change the target location")] [SerializeField]
    private float minMouseDistanceAway = 2.0f;
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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePosition - transform.position);
            float distance = direction.magnitude;
            direction = direction.normalized;

            Vector3 targetLocation = target.transform.position;
            if (distance > minMouseDistanceAway)
            {
                targetLocation += direction * (distance * persentOfDistanceFromMouse);
            }

            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Lerp(newPosition.x, targetLocation.x, lerpSpeed);
            newPosition.y = Mathf.Lerp(newPosition.y, targetLocation.y, lerpSpeed);
            transform.position = newPosition;

            Vector2 distanceFromTargetLocation = transform.position - target.transform.position;
            if (distanceFromTargetLocation.magnitude > maxDistance)
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
