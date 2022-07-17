using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerController : BaseEnemy
{
    private Transform crossbow;

    [SerializeField] private float prepSpeed;
    [SerializeField] private float lowBound;
    [SerializeField] private float highBound;
    [SerializeField] private GameObject boltPrefab;
    [SerializeField] private float boltSpeed = 8;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        crossbow = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
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
                case float n when n > highBound:
                    state = "follow";
                    break;
                case float n when n >= lowBound:
                    state = "prep_shoot";
                    break;
                default:
                    state = "run";
                    break;
            }
        }

        //Debug.Log(state);

        switch (state) 
        {
            case "idle":
                EnemyRB.velocity = Vector3.zero;
                state = null;
                break;


            case "follow":
                virtuallyFacing = new Vector2(direction.x, direction.y).normalized;

                EnemyRB.velocity = virtuallyFacing * speed;
                state = null;
                state_time = 0;

                GetComponent<SpriteRenderer>().flipX = (EnemyRB.velocity.x < 0);
                crossbow.GetChild(0).GetComponent<SpriteRenderer>().flipY = (EnemyRB.velocity.x < 0);

                crossbow.localPosition = new Vector3(0.0f, -0.35f, 0.0f);
                crossbow.GetChild(0).localPosition = new Vector3(0.25f, 0.0f, 0.0f);
                crossbow.right = direction;

                break;


            case "prep_shoot":
                virtuallyFacing = new Vector2(direction.x, direction.y).normalized;

                if (distance > highBound)
                {
                    EnemyRB.velocity = virtuallyFacing * prepSpeed;
                }
                else if (distance < lowBound)
                {
                    EnemyRB.velocity = -virtuallyFacing * prepSpeed;
                }
                else
                {
                    EnemyRB.velocity = Vector3.zero;
                }

                GetComponent<SpriteRenderer>().flipX = (direction.x < 0);
                crossbow.GetChild(0).GetComponent<SpriteRenderer>().flipY = (direction.x < 0);

                crossbow.localPosition = new Vector3(0.0f, -0.025f, 0.0f);
                crossbow.GetChild(0).localPosition = new Vector3(0.5f, 0.0f, 0.0f);
                crossbow.right = direction;

                if (state_time >= 60) { state = "shoot"; state_time = 0; }

                break;

            case "shoot":
                if (state_time == 1) {
                    GameObject bullet = Instantiate(boltPrefab, crossbow.GetChild(0).transform.position, crossbow.transform.rotation);
                    Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

                    bulletRB.velocity = (player.transform.position - bullet.transform.position).normalized * boltSpeed;
                }
                else if (state_time >= 20)
                {
                    state = null;
                    state_time = 0;
                }
                break;

            case "run":
                virtuallyFacing = -1 * new Vector2(direction.x, direction.y).normalized;

                EnemyRB.velocity = virtuallyFacing * speed;
                state = null;
                state_time = 0;

                GetComponent<SpriteRenderer>().flipX = (EnemyRB.velocity.x < 0);
                crossbow.GetChild(0).GetComponent<SpriteRenderer>().flipY = (EnemyRB.velocity.x < 0);

                crossbow.localPosition = new Vector3(Mathf.Sign(direction.x) * 0.3f, 0.6f, 0.0f);
                crossbow.GetChild(0).localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                crossbow.right = Vector3.up;

                break;


            default:
                EnemyRB.velocity = Vector2.zero;
                state = null;
                break;
        }

        state_time += 1;
    }
}
