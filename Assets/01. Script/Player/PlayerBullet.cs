using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullets
{
    Rigidbody2D _rigid2d;
    float speed = 6;
    private void Awake()
    {
        _rigid2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);
    }

    void Update()
    {
        _rigid2d.velocity = dir.normalized * speed;
    }

    public override void CollisionAny(Collider2D collision)
    {
        if (collision.CompareTag("BulletBorder"))
        {
            PoolManager.Release(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionAny(collision);
    }
}
