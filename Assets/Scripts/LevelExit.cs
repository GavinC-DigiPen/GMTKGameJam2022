//------------------------------------------------------------------------------
//
// File Name:	LevelExit.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [Tooltip("The key to exit the level")] [SerializeField]
    private KeyCode interactKey = KeyCode.E;
    [Tooltip("The name of the scene that will be loaded")] [SerializeField]
    private string sceneName;

    private bool inArea;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey) && inArea)
        {
            SceneManager.LoadScene(sceneName);
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
