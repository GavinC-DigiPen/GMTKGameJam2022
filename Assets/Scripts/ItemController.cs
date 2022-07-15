using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public KeyCode key;
    private List<GameObject> touching;

    void Start() 
    {
        touching = new List<GameObject> ();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject GO = col.gameObject;

        touching.Add(GO);
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        GameObject GO = col.gameObject;

        touching.Remove(GO);
    }

    void Update()
    {
        bool keyPressed = Input.GetKeyDown(key);

        if (keyPressed) {
            if (touching.Count != 0) {
                GameManger.primaryWeapon = touching[0];
            }
        }
    }
}
