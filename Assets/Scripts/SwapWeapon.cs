using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeapon : MonoBehaviour
{
    [Tooltip("The key used to swap weapons")] [SerializeField]
    private KeyCode swapKey = KeyCode.Q;
    [Tooltip("The cooldown for swapping weapons")] [SerializeField]
    private float swapCooldown = 0.5f;

    private float swapTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (swapTimer <= 0 && Input.GetKeyDown(swapKey))
        {
            GameObject temp = GameManger.secondaryWeapon;
            GameManger.secondaryWeapon = GameManger.primaryWeapon;
            GameManger.primaryWeapon = temp;

            if (GameManger.primaryWeapon != null)
            {
                GameManger.primaryWeapon.SetActive(true);
            }
            if (GameManger.secondaryWeapon != null)
            {
                GameManger.secondaryWeapon.SetActive(false);
            }

            swapTimer = swapCooldown;
        }
        else if (swapTimer > 0)
        {
            swapTimer -= Time.deltaTime;
        }
    }
}
