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
    [Tooltip("The child that is the image of the gun")] [SerializeField]
    private GameObject gunImage;
    [Tooltip("The prefab of the bullet")]
    public GameObject bulletPrefab;
    [Tooltip("The number of bullets to spawn")]
    public int numBullets = 1;
    [Tooltip("The sprite used as the icon for the gun")]
    public Sprite gunIcon;
    [Tooltip("The time between bullet shots")] [SerializeField]
    private float shotCooldown = 0.5f;
    [Tooltip("The amount of kickback the gun has")] [SerializeField]
    private float kickback = 0.1f;
    [Tooltip("All the bullet's value (not all are used with all weapons")]
    public List<int> nextBulletValue;

    private float shotTimer = 0.0f;

    public static UnityEvent DiceRollUpdate = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        nextBulletValue = new List<int>();
        for (int i = 0; i < numBullets; i++)
        {
            nextBulletValue.Add(1);
        }

        RollNextDice();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotating gun
        if (isHeld)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;
            direction = direction.normalized;
            float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rotation));
            gunImage.GetComponent<SpriteRenderer>().flipX = (rotation < 0);

            // Shooting gun
            if (shotTimer <= 0 && Input.GetMouseButton(0))
            {
                Shoot();
                Debug.Log(direction);
                gunImage.transform.localPosition = gunImage.transform.localPosition - new Vector3(((rotation > 0) ? 1 : -1), 1, 0) * kickback;
                shotTimer = shotCooldown;
            }
            if (shotTimer > 0)
            {
                shotTimer -= Time.deltaTime;
            }
            gunImage.transform.localPosition = Vector3.Lerp(gunImage.transform.localPosition, Vector3.zero, 0.01f);
        }
    }

    // Set the value of the dice
    virtual protected void RollNextDice()
    {
        nextBulletValue[0] = Random.Range(1, bulletPrefab.GetComponent<Bullet>().numSides + 1);
        DiceRollUpdate.Invoke();
    }

    // Shoot the gun
    virtual protected void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        newBullet.GetComponent<Bullet>().rolledValue = nextBulletValue[0];
        newBullet.GetComponent<Bullet>().Shoot();
        RollNextDice();
    }
}
