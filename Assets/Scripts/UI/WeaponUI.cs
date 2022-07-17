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
    [Tooltip("The primary weapon base damage game object")] [SerializeField]
    private GameObject primaryWeaponBaseDamageObject; 
    [Tooltip("The secondary weapon game object")] [SerializeField]
    private GameObject secondaryWeaponObject;
    [Tooltip("The secondary weapon dice game objects")] [SerializeField]
    private GameObject[] secondaryWeaponDiceObjects;
    [Tooltip("The secondary weapon base damage game object")] [SerializeField]
    private GameObject secondaryWeaponBaseDamageObject;
    [Tooltip("The numbers used to display base damage")] [SerializeField]
    private Sprite[] numberSprites;
    [Tooltip("The particle effect object for good roll")] [SerializeField]
    private GameObject goodRollParticle;
    [Tooltip("The particle effect object for bad roll")] [SerializeField]
    private GameObject badRollParticle;

    // Start is called before the first frame update
    void Start()
    {
        UpdateWeaponUI();
        GameManger.PrimaryWeaponUpdate.AddListener(UpdateWeaponUI);
        Gun.DiceRollUpdate.AddListener(UpdateDiceRollUI);
    }

    void UpdateWeaponUI()
    {
        // Primary
        if (GameManger.primaryWeapon != null)
        {
            primaryWeaponObject.SetActive(true);
            primaryWeaponObject.GetComponent<Image>().sprite = GameManger.primaryWeapon.GetComponent<Gun>().gunWithoutHands;

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

            primaryWeaponBaseDamageObject.GetComponent<Image>().sprite = numberSprites[(GameManger.primaryWeapon.GetComponent<Gun>().baseDamage > numberSprites.Length - 1) ? numberSprites.Length - 1 : GameManger.primaryWeapon.GetComponent<Gun>().baseDamage];
        }
        else
        {
            primaryWeaponObject.SetActive(false);
        }

        // Secondary
        if (GameManger.secondaryWeapon != null)
        {
            secondaryWeaponObject.SetActive(true);
            secondaryWeaponObject.GetComponent<Image>().sprite = GameManger.secondaryWeapon.GetComponent<Gun>().gunWithoutHands;

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

            secondaryWeaponBaseDamageObject.GetComponent<Image>().sprite = numberSprites[(GameManger.secondaryWeapon.GetComponent<Gun>().baseDamage > numberSprites.Length - 1) ? numberSprites.Length - 1 : GameManger.secondaryWeapon.GetComponent<Gun>().baseDamage];
        }
        else
        {
            secondaryWeaponObject.SetActive(false);
        }

        UpdateDiceRollUI();
    }

    void UpdateDiceRollUI()
    {
        // Primary
        if (GameManger.primaryWeapon != null)
        {
            Gun primaryGun = GameManger.primaryWeapon.GetComponent<Gun>();
            for (int i = 0; i < primaryGun.numBullets && i < primaryWeaponDiceObjects.Length; i++)
            {
                primaryWeaponDiceObjects[i].GetComponent<Image>().sprite = primaryGun.bulletPrefab.GetComponent<Bullet>().sprites[primaryGun.nextBulletValue[i] - 1];

                if (primaryGun.nextBulletValue[i] == primaryGun.bulletPrefab.GetComponent<Bullet>().numSides)
                {
                    Instantiate(goodRollParticle, primaryWeaponDiceObjects[i].transform);
                }
                if (primaryGun.nextBulletValue[i] == 1)
                {
                    Instantiate(badRollParticle, primaryWeaponDiceObjects[i].transform);
                }
            }
        }

        // Secondary
        if (GameManger.secondaryWeapon != null)
        {
            Gun secondaryGun = GameManger.secondaryWeapon.GetComponent<Gun>();
            for (int i = 0; i < secondaryGun.numBullets && i < secondaryWeaponDiceObjects.Length; i++)
            {
                secondaryWeaponDiceObjects[i].GetComponent<Image>().sprite = secondaryGun.bulletPrefab.GetComponent<Bullet>().sprites[secondaryGun.nextBulletValue[i] - 1];
            }
        }
    }
}
