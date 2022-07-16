//------------------------------------------------------------------------------
//
// File Name:	WeaponUI.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [Tooltip("The primary weapon game object")] [SerializeField]
    private GameObject primaryWeaponObject;
    [Tooltip("The primary weapon dice game objects")] [SerializeField]
    private GameObject[] primaryWeaponDiceObjects;
    [Tooltip("The secondary weapon game object")] [SerializeField]
    private GameObject secondaryWeaponObject;
    [Tooltip("The secondary weapon dice game objects")] [SerializeField]
    private GameObject[] secondaryWeaponDiceObjects;

    // Start is called before the first frame update
    void Start()
    {
        UpdateWeaponUI();
        GameManger.PrimaryWeaponUpdate.AddListener(UpdateWeaponUI);
        Gun.DiceRollUpdate.AddListener(UpdateDiceRollUI);
    }

    void UpdateWeaponUI()
    {
        UpdateDiceRollUI();

        // Primary
        if (GameManger.primaryWeapon != null)
        {
            primaryWeaponObject.SetActive(true);
            primaryWeaponObject.GetComponent<Image>().sprite = GameManger.primaryWeapon.GetComponent<Gun>().gunIcon;

            for (int i = 0; i < primaryWeaponDiceObjects.Length; i++)
            {
                if (i < GameManger.primaryWeapon.GetComponent<Gun>().numBullets)
                {
                    primaryWeaponDiceObjects[i].SetActive(true);
                }
                else
                {
                    primaryWeaponDiceObjects[i].SetActive(false);
                }
            }
        }
        else
        {
            primaryWeaponObject.SetActive(false);
        }

        // Secondary
        if (GameManger.secondaryWeapon != null)
        {
            secondaryWeaponObject.SetActive(true);
            secondaryWeaponObject.GetComponent<Image>().sprite = GameManger.secondaryWeapon.GetComponent<Gun>().gunIcon;

            for (int i = 0; i < secondaryWeaponDiceObjects.Length; i++)
            {
                if (i < GameManger.secondaryWeapon.GetComponent<Gun>().numBullets)
                {
                    secondaryWeaponDiceObjects[i].SetActive(true);
                }
                else
                {
                    secondaryWeaponDiceObjects[i].SetActive(false);
                }
            }
        }
        else
        {
            secondaryWeaponObject.SetActive(false);
        }
    }

    void UpdateDiceRollUI()
    {
        // Primary
        if (GameManger.primaryWeapon != null)
        {
            Gun primaryGun = GameManger.primaryWeapon.GetComponent<Gun>();
            for (int i = 0; i < primaryGun.numBullets; i++)
            {
                primaryWeaponDiceObjects[i].GetComponent<Image>().sprite = primaryGun.bulletPrefab.GetComponent<Bullet>().sprites[primaryGun.nextBulletValue[i] - 1];
            }
        }

        // Secondary
        if (GameManger.secondaryWeapon != null)
        {
            Gun secondaryGun = GameManger.secondaryWeapon.GetComponent<Gun>();
            for (int i = 0; i < secondaryGun.numBullets; i++)
            {
                secondaryWeaponDiceObjects[i].GetComponent<Image>().sprite = secondaryGun.bulletPrefab.GetComponent<Bullet>().sprites[secondaryGun.nextBulletValue[i] - 1];
            }
        }
    }
}
