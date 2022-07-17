//------------------------------------------------------------------------------
//
// File Name:	Gun.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [Tooltip("If the gun is being held or not")]
    public bool isHeld = false;
    [Tooltip("The base damage of the gun")]
    public int baseDamage = 1;
    [Tooltip("The number of bullets to spawn")]
    public int numBullets = 1;
    [Tooltip("Should the bullets shoot from the same location or the location the gun is at the moment they are released")] [SerializeField]
    private bool shootAllBulletsFromSamePosition = true;
    [Tooltip("Time inbetween bullets being released")] [SerializeField]
    private float timeBetweenBullets = 0.1f;
    [Tooltip("The time between bullet shots")] [SerializeField]
    private float shotCooldown = 0.5f;
    [Tooltip("The amount of kickback the gun has visually")] [SerializeField]
    private float visualKickback = 0.1f;
    [Tooltip("The amount of kickback the gun has")] [SerializeField]
    private float kickback = 1.0f;
    [Tooltip("The prefab of the bullet")]
    public GameObject bulletPrefab;
    [Tooltip("The sprite used as the icon for the gun")]
    public Sprite gunIcon;
    [Tooltip("The child that is the image of the gun")] [SerializeField]
    private GameObject gunImage;
    [Tooltip("The sound that plays when the gun is shot")] [SerializeField]
    protected AudioClip gunSound;
    [Tooltip("The particle effect object that is created on fire")] [SerializeField]
    private GameObject gunParticle;
    [Tooltip("Gun particle effect location")] [SerializeField]
    protected Transform endOfGun;
    [Tooltip("All the bullet's value (not all are used with all weapons")]
    public List<int> nextBulletValue;

    private static float shotTimer = 0.0f;
    private Vector2 direction;
    protected Vector2 velocityFromPlayer;

    public static UnityEvent DiceRollUpdate = new UnityEvent();

    private SpriteRenderer gunImageSpriteSource;

    // Start is called before the first frame update
    void Start()
    {
        gunImageSpriteSource = gunImage.GetComponent<SpriteRenderer>();

        nextBulletValue = new List<int>();
        for (int i = 0; i < numBullets; i++)
        {
            nextBulletValue.Add(1);
        }

        float rotation = Random.Range(-360, 360);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));

        RollNextDice();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            // Rotating gun
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rotation));
            gunImageSpriteSource.flipX = (rotation < 0);

            // Shooting gun
            if (shotTimer <= 0 && Input.GetMouseButton(0))
            {
                GetVelocityFromPlayer();
                Shoot();
                shotTimer = shotCooldown;
            }
            if (shotTimer > 0)
            {
                shotTimer -= Time.deltaTime;
            }

            // Undo kickback
            gunImage.transform.localPosition = Vector3.Lerp(gunImage.transform.localPosition, Vector3.zero, 0.01f);
        }
    }

    // Do kickback
    protected void Recoil()
    {
        Instantiate(gunParticle, endOfGun);
        //gunImage.transform.localPosition = gunImage.transform.localPosition - new Vector3(((transform.rotation.eulerAngles.z > 0) ? 1 : -1), ((transform.rotation.eulerAngles.z > 0) ? 0.7f : -0.7f), 0) * visualKickback;
        gunImage.transform.localPosition = gunImage.transform.localPosition - new Vector3((!gunImageSpriteSource.flipX ? 1 : -1), 1, 0) * visualKickback;
        GameManger.player.GetComponent<Rigidbody2D>().velocity += -direction * kickback;
    }

    // Get velocity to add to bullets
    void GetVelocityFromPlayer()
    {
        Rigidbody2D playerRB = GameManger.player.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = transform.up;
        if ((bulletVelocity.x > 0 && playerRB.velocity.x > 0) || (bulletVelocity.x < 0 && playerRB.velocity.x < 0))
        {
            bulletVelocity.x += playerRB.velocity.x;
        }
        if ((bulletVelocity.y > 0 && playerRB.velocity.y > 0) || (bulletVelocity.y < 0 && playerRB.velocity.y < 0))
        {
            bulletVelocity.y += playerRB.velocity.y;
        }
        velocityFromPlayer = bulletVelocity;
    }

    // Set the value of the dice
    virtual protected void RollNextDice()
    {
        for (int i = 0; i < numBullets; i++)
        {
            nextBulletValue[i] = Random.Range(1, bulletPrefab.GetComponent<Bullet>().numSides + 1);
        }
        DiceRollUpdate.Invoke();
    }

    // Shoot the gun
    virtual protected void Shoot()
    {
        int i = 0;
        for (; i < numBullets; i++)
        {
            if (gunSound)
            {
                GameManger.player.GetComponent<AudioSource>().PlayOneShot(gunSound);
            }

            if (shootAllBulletsFromSamePosition)
            {
                StartCoroutine(InstantiateBulletWithVariables(bulletPrefab, endOfGun.position, transform.rotation, i));
            }
            else
            {
                StartCoroutine(InstantiateBullet(i));
            }
        }
        Invoke("RollNextDice", i * timeBetweenBullets + 0.01f);
    }

    IEnumerator InstantiateBulletWithVariables(GameObject obj, Vector3 position, Quaternion rotation, int index)
    {
        yield return new WaitForSeconds(index * timeBetweenBullets);

        Bullet newBulletScript = Instantiate(obj, position, rotation).GetComponent<Bullet>();

        newBulletScript.baseDamage = baseDamage;
        newBulletScript.rolledValue = nextBulletValue[index];
        newBulletScript.playerVelocityToAdd = velocityFromPlayer;
        newBulletScript.Shoot();
        Recoil();
    }

    IEnumerator InstantiateBullet(int index)
    {
        yield return new WaitForSeconds(index * timeBetweenBullets);

        Bullet newBulletScript = Instantiate(bulletPrefab, endOfGun.position, transform.rotation).GetComponent<Bullet>();

        newBulletScript.baseDamage = baseDamage;
        newBulletScript.rolledValue = nextBulletValue[index];
        newBulletScript.playerVelocityToAdd = velocityFromPlayer;
        newBulletScript.Shoot();
        Recoil();
    }
}
