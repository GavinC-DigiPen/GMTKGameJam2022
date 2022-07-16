//------------------------------------------------------------------------------
//
// File Name:	InteractPopUp.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPopUp : MonoBehaviour
{
    [Tooltip("The hidden pop-up")] [SerializeField]
    private GameObject hiddenObject;
    [Tooltip("The distance up the pop-up is away from the object")] [SerializeField]
    private float distanceAway = 1.0f;

    private bool inArea = false;
    private bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        // Set Active
        if (GetComponent<Gun>() != null && !GetComponent<Gun>().isHeld)
        {
            isActive = true;
        }
        else if (GetComponent<LootBox>() != null && !GetComponent<LootBox>().isOpen)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }

        // Hide or un-hide
        if (inArea && isActive)
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
        if (collision.gameObject.GetComponent<PlayerController>() != null)
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
