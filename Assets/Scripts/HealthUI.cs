//------------------------------------------------------------------------------
//
// File Name:	HealthUI.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Tooltip("The objects that represent the hearts (you need a number equal to half max health)")] [SerializeField]
    private GameObject[] heartObjects;
    [Tooltip("The three heart sprites (full, half, empty)")] [SerializeField]
    private Sprite[] heartSprites;

    private int healthPerHeart = 2;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHeartUI();
        GameManger.CurrentHealthUpdate.AddListener(UpdateHeartUI);
    }

    // Update the heart UI
    void UpdateHeartUI()
    {
        for (int i = 1; i <= heartObjects.Length; i++)
        {
            if (GameManger.currentHealth >= i * healthPerHeart)
            {
                heartObjects[i - 1].GetComponent<Image>().sprite = heartSprites[0];
            }
            else if (GameManger.currentHealth - ((i - 1) * healthPerHeart) == 1)
            {
                heartObjects[i - 1].GetComponent<Image>().sprite = heartSprites[1];
            }
            else
            {
                heartObjects[i - 1].GetComponent<Image>().sprite = heartSprites[2];
            }
        }
    }
}
