using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected GameObject player;
    protected Rigidbody2D EnemyRB;

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
