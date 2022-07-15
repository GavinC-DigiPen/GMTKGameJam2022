using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public KeyCode key;
    private List<GameObject> touchingGuns;

    void Start() 
    {
        touchingGuns = new List<GameObject> ();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject GO = col.gameObject;

        bool isGun = GO.GetComponent<Gun>() != null;

        if (isGun) 
        {
            touchingGuns.Add(GO);
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        GameObject GO = col.gameObject;

        bool isGun = GO.GetComponent<Gun>() != null;

        if (isGun)
        {
            touchingGuns.Remove(GO);
        }
    }

    void Update()
    {
        bool keyPressed = Input.GetKeyDown(key);

        if (keyPressed) 
        {
            if (touchingGuns.Count != 0) 
            {
                if (GameManger.primaryWeapon != null)
                {
                    GameManger.primaryWeapon.GetComponent<Gun>().isHeld = false;
                }

                GameManger.primaryWeapon = touchingGuns[0];
                touchingGuns[0].GetComponent<Gun>().isHeld = true;
            }
        }
    }
}
