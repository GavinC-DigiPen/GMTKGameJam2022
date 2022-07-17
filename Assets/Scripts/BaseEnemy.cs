using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected Vector2 virtuallyFacing;

    protected GameObject player;
    protected Rigidbody2D EnemyRB;

    [SerializeField]
    protected float speed = 1;
    [SerializeField]
    protected float agroRange = 15;

    public string state = "follow";
    public int state_time = 0;
    virtual protected void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        EnemyRB = GetComponent<Rigidbody2D>();
    }

    void pushBack() {
        state = "push_back";
        state_time = 0;
    }
}
