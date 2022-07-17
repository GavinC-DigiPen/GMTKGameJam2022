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
    [Tooltip("The particle effect object that is created on use")] [SerializeField]
    private GameObject useParticle;
    [Tooltip("The name of the scene that will be loaded")] [SerializeField]
    private string exitScene;
    [Tooltip("The delay before scene transition")] [SerializeField]
    private float leaveTimeDelay = 1.0f;

    private bool inArea;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey) && inArea)
        {
            StartCoroutine(EndGame());
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

    IEnumerator EndGame()
    {
        Instantiate(useParticle, transform);

        // Push enemies
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();
        foreach (BaseEnemy enemy in enemies)
        {
            if ((enemy.transform.position - gameObject.transform.position).magnitude <= 5)
            {
                enemy.state = "push back";
                enemy.state_time = 0;
            }
        }

        // Disable gun
        if (GameManger.primaryWeapon != null)
        {
            Destroy(GameManger.primaryWeapon.GetComponent<Gun>());
            Destroy(GameManger.primaryWeapon.GetComponent<InteractPopUp>());
        }

        // Disable player
        Destroy(GameManger.player.GetComponent<BoxCollider2D>());
        Destroy(GameManger.player.GetComponent<PlayerController>());
        Destroy(GameManger.player.GetComponent<HoldingGun>());
        Destroy(GameManger.player.GetComponent<SwapWeapon>());
        Destroy(GameManger.player.transform.GetChild(0).GetComponent<GunPickUp>());
        GameManger.player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        yield return new WaitForSeconds(leaveTimeDelay);
        SceneManager.LoadScene(exitScene);
    }
}
