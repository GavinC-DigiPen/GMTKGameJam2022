using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private float speed = 1;

    public string state = "follow";
    public int state_time = 0;

    public int maxHealth = 5;
    public int currentHealth;

    private Vector2 virtuallyFacing;
    private Rigidbody2D EnemyRB;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        EnemyRB = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        Vector3 dif = player.transform.position - transform.position;
        Vector3 direction = dif.normalized;
        float distance = dif.magnitude;

        if (state == null) {
            state_time = 0;

            switch (distance)
            {
                case float n when n < 3:
                    state = "prep lunge";
                    break;
                default:
                    state = "follow";
                    break;
            }
        }

        switch (state)
        {
            case "follow":

                virtuallyFacing = new Vector2(direction.x, direction.y).normalized;
                EnemyRB.velocity = virtuallyFacing * speed;
                state = null;
                break;

            case "prep lunge":
                if (state_time == 1)
                {
                    EnemyRB.velocity = Vector2.zero;
                    virtuallyFacing = direction.normalized;
                }
                if (state_time >= 15)
                {
                    state = "lunge";
                    state_time = 0;
                }
                break;
            case "lunge":

                if (state_time <= 15)
                {
                    EnemyRB.velocity = virtuallyFacing * speed * 5;
                }
                else
                {
                    EnemyRB.velocity = virtuallyFacing * speed * (25 - state_time) / 2;
                }

                if (state_time >= 25)
                {
                    state = "post lunge";
                    state_time = 0;
                }
                break;
            case "post lunge":
                if (state_time >= 20)
                {
                    state = null;
                }
                break;
            case "push back":
                if (state_time == 1) {
                    virtuallyFacing = direction.normalized;
                }

                if (state_time <= 20)
                {
                    EnemyRB.velocity = virtuallyFacing * speed * -2;
                }
                else {
                    EnemyRB.velocity = virtuallyFacing * speed * (40 - state_time) / -10.0f;
                }
                if (state_time >= 40)
                {
                    state = "post push back";
                    state_time = 0;
                }
                break;
            case "post push back":
                if (state_time >= 40)
                {
                    state = null;
                }
                break;
        }

        state_time += 1;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
