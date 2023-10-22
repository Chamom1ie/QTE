using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierBullet : Bullets
{
    Rigidbody2D _rigid2d;
    float speed = 7;
    private void Awake()
    {
        _rigid2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigid2d.velocity = dir.normalized * speed;
    }

    public override void SetDir(Vector3 moveDir)
    {
        dir = moveDir;
        gameObject.tag = "Go";

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionAny(collision);
    }

    public override void CollisionAny(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.GetComponent<Player>().OnHit(damage);
        }
        if (collision.gameObject.CompareTag("BulletBorder"))
        {
            Destroy(gameObject);
        }
    }
}
