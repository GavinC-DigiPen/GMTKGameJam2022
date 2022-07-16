//------------------------------------------------------------------------------
//
// File Name:	PickUpPopUps.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPopUps : MonoBehaviour
{
    [Tooltip("The hidden pop-up")] [SerializeField]
    private GameObject hiddenObject;
    [Tooltip("The distance up the pop-up is away from the gun")] [SerializeField]
    private float distanceAway = 1.0f;

    private bool inArea = false;

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Gun>().isHeld && inArea)
        {
            hiddenObject.transform.position = transform.position + Vector3.up * distanceAway;
            hiddenObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -transform.rotation.eulerAngles.z));
            hiddenObject.SetActive(true);
        }
        else
        {
            hiddenObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null && !GetComponent<Gun>().isHeld)
        {
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            inArea = false;
        }
    }
}
