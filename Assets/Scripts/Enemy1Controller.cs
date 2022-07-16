using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float speed = 1;

    public string state = "follow";
    public int state_time = 0;

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
                transform.up = direction.normalized;
                transform.Translate(Vector3.up * speed);
                state = null;
                break;
            case "prep lunge":
                if (state_time == 1)
                {
                    transform.up = direction.normalized;
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
                    transform.Translate(Vector3.up * speed * 5);
                }
                else
                {
                    transform.Translate(Vector3.up * speed * (25 - state_time) / 2);
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
                    transform.up = direction.normalized;
                }

                if (state_time <= 20)
                {
                    transform.Translate(Vector3.up * speed * -2);
                }
                else {
                    transform.Translate(Vector3.up * speed * (40 - state_time) / -10.0f);
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

        /*
        switch (distance)
        {
            case float n when n >= 1:
                transform.up = direction.normalized;
                transform.Translate(Vector3.up * speed);
                break;
            case float n when n < 1:
                transform.up = direction.normalized;
                break;
            default: break;
        }*/
    }
}
