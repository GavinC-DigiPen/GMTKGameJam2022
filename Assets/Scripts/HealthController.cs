using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{    
    private void Start()
    {
        GameManger.CurrentHealthUpdate.AddListener(pushBack);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision Detected");

        if (col.gameObject.tag == "Damaging")
        {
            Debug.Log("Damage Detected");

            GameManger.currentHealth -= 1;
        }
    }

    private void pushBack()
    {
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
}
