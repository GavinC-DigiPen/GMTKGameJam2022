//------------------------------------------------------------------------------
//
// File Name:	LootBox.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [Tooltip("All the prefabs that could drop form the box")] [SerializeField]
    private GameObject[] possibleDrops;
    [Tooltip("The offset for where the item will be dropped")] [SerializeField]
    private Vector2 dropOffset;
    [Tooltip("The key to open the box")] [SerializeField]
    private KeyCode interactKey = KeyCode.E;
    [Tooltip("The open box sprite")] [SerializeField]
    private Sprite openBox;
    [Tooltip("If the box is open (For scripts)")]
    public bool isOpen = false;

    private bool inArea;

    // Start is called before the first frame update
    void Start()
    {
        if (isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = openBox;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey) && inArea && !isOpen)
        {
            isOpen = true;
            GetComponent<SpriteRenderer>().sprite = openBox;

            int index = Random.Range(0, possibleDrops.Length);
            float rotation = Random.Range(-360, 360);
            Instantiate(possibleDrops[index], transform.position + (Vector3)dropOffset, Quaternion.Euler(new Vector3(0, 0, rotation)));
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
