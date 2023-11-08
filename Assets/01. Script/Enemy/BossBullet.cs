using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Bullets
{
    readonly float speed = 7;

    private void Awake()
    {
        _rigid2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigid2d.velocity = dir.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionAny(collision);
    }
}
