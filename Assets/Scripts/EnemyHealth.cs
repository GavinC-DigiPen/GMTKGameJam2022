//------------------------------------------------------------------------------
//
// File Name:	EnemyHealth.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [Tooltip("The max enemy health")]
    public int maxHealth = 5;
    [Tooltip("The amount of damage delt on contact")]
    public int contactDamage = 1;
    [Tooltip("The sound that plays when hit")] [SerializeField]
    protected AudioClip hitSound;
    [Tooltip("The object that will be created to make sound")] [SerializeField]
    protected GameObject soundObject;

    private int _currentHealth;
    public int currentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            HealthChange();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
    }

    void HealthChange()
    {
        Instantiate(soundObject,transform.position, Quaternion.identity).GetComponent<AudioSource>().PlayOneShot(hitSound);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
