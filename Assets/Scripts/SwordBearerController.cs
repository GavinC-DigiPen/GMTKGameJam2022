using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBearerController : BaseEnemy
{    
    private Transform sword;
    private float swordRotation = -9.0f;
    override protected void Start()
    {
        base.Start();

        sword = gameObject.transform.GetChild(0);
    }

    void FixedUpdate()
    {
        Vector3 dif = player.transform.position - transform.position;
        Vector3 direction = dif.normalized;
        float distance = dif.magnitude;

        if (state == null)
        {
            state_time = 0;

            switch (distance)
            {
                case float n when n > agroRange:
                    state = "idle";
                    break;
                case float n when n < 2:
                    state = "prep attack";
                    break;
                default:
                    state = "follow";
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) 
        {
            state = "prep_attack";
            state_time = 0;
        }

        switch (state)
        {
            case "idle":
                sword.rotation = Quaternion.identity;
                sword.right = Vector3.up;
                sword.localPosition = Vector3.zero;
                EnemyRB.velocity = Vector3.zero;
                state = null;

                break;

            case "follow":

                virtuallyFacing = new Vector2(direction.x, direction.y).normalized;

                sword.rotation = Quaternion.identity;
                sword.right = Vector3.up;
                sword.localPosition = (player.transform.position - gameObject.transform.position).normalized / 3;

                sword.GetChild(0).localPosition = new Vector3(0.75f, 0.0f, 0.0f);

                EnemyRB.velocity = virtuallyFacing * speed;
                state = null;

                GetComponent<SpriteRenderer>().flipX = (EnemyRB.velocity.x < 0);

                break;

            case "prep attack":
                if (state_time == 1)
                {
                    sword.right = Vector3.up;

                    sword.GetChild(0).localPosition = new Vector3(1.25f, 0.0f, 0.0f);

                    GetComponent<SpriteRenderer>().flipX = (EnemyRB.velocity.x < 0);

                    if (player.transform.position.x < transform.position.x)
                    {
                        swordRotation = 9.0f;
                    }
                    else
                    {
                        swordRotation = -9.0f;
                    }
                }

                float trembleRange = 0.05f;
                sword.localPosition = new Vector3(Random.Range(-trembleRange, trembleRange), Random.Range(-trembleRange, trembleRange), 0);

                virtuallyFacing = new Vector2(direction.x, direction.y).normalized;
                EnemyRB.velocity = virtuallyFacing * speed / 2;

                if (state_time >= 20)
                {
                    state = "attack";
                    state_time = 0;
                }
                break;
            case "attack":
                if (state_time == 1)
                {
                    sword.localPosition = Vector3.zero;

                    sword.GetChild(0).localPosition = new Vector3(1.25f, 0.0f, 0.0f);
                }
                if (state_time <= 20)
                {
                    sword.Rotate(new Vector3(0.0f, 0.0f, swordRotation));

                    virtuallyFacing = new Vector2(direction.x, direction.y).normalized;
                    EnemyRB.velocity = virtuallyFacing * speed/2;
                }
                else 
                {
                    state = "post attack";
                }
                break;
            case "post attack":
                                virtuallyFacing = new Vector2(direction.x, direction.y).normalized;
                EnemyRB.velocity = virtuallyFacing * speed / 2;

                if (state_time >= 40)
                {
                    state = null;
                    state_time = 0;
                }
                break;

            case "push back":
                if (state_time == 1)
                {
                    virtuallyFacing = direction.normalized;
                }

                if (state_time <= 20)
                {
                    EnemyRB.velocity = virtuallyFacing * speed * -2;
                }
                else
                {
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
}
