using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Damaging") 
        {
            GameManger.currentHealth -= 1;

            Enemy1Controller[] enemies = FindObjectsOfType<Enemy1Controller>();

            foreach (Enemy1Controller enemy in enemies)
            {
                if ((enemy.transform.position - gameObject.transform.position).magnitude <= 5)
                {
                    enemy.state = "push back";
                    enemy.state_time = 0;
                }
            }
        }
    }
}
