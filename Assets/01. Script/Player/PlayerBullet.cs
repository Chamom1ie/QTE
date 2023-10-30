using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullets
{
    float speed = 6;
    private void Awake()
    {
        _rigid2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigid2d.velocity = dir.normalized * speed;
    }

    public override void CollisionAny(Collider2D collision)
    {
        if (collision.CompareTag("BulletBorder"))
        {
            dir = Vector3.zero;
            _rigid2d.velocity = Vector2.zero;
            PoolManager.Release(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionAny(collision);
    }
}
