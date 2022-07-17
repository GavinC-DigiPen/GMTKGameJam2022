using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [Tooltip("The particle effect object that is created on hurt")] [SerializeField]
    private GameObject hurtParticle;
    [Tooltip("The sound that plays when hit")] [SerializeField]
    protected AudioClip hitSound;
    [Tooltip("The object that will be created to make sound")] [SerializeField]
    protected GameObject soundObject;
    [Tooltip("The particle effect object that is created on death")] [SerializeField]
    private GameObject deathParticle;
    [Tooltip("The scene that is loaded when you die")] [SerializeField]
    private string deathScene;
    [Tooltip("The delay before scene transition")] [SerializeField]
    private float deathTimeDelay = 1.0f;

    private void Start()
    {
        GameManger.CurrentHealthUpdate.AddListener(HealthChange);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Damaging")
        {
            GameManger.currentHealth -= 1;
        }
    }

    private void HealthChange()
    {
        // Particle
        if (GameManger.currentHealth != GameManger.maxHealth)
        {
            Instantiate(hurtParticle, transform);
            Instantiate(soundObject, transform.position, Quaternion.identity).GetComponent<AudioSource>().PlayOneShot(hitSound);
        }

        // Checklife
        if (GameManger.currentHealth <= 0)
        {
            StartCoroutine(EndGame());
        }

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
    }

    IEnumerator EndGame()
    {
        Instantiate(deathParticle, transform);

        // Disable gun
        if (GameManger.primaryWeapon != null)
        {
            Destroy(GameManger.primaryWeapon.GetComponent<Gun>());
            Destroy(GameManger.primaryWeapon.GetComponent<InteractPopUp>());
        }

        // Disable player
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<PlayerController>());
        Destroy(GetComponent<HoldingGun>());
        Destroy(GetComponent<SwapWeapon>());
        Destroy(transform.GetChild(0).GetComponent<GunPickUp>());
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        yield return new WaitForSeconds(deathTimeDelay);
        SceneManager.LoadScene(deathScene);
    }
}
