using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LinearBullet : Bullets
{
    Rigidbody2D _rigid2d;
    float speed = 8.5f;
    private void Awake()
    {
        _rigid2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigid2d.velocity = dir.normalized * speed;
    }
    public async void SetDir(Vector3 moveDir, int millisec)
    {
        await Task.Delay(millisec * 100);
        dir = moveDir;
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
    }
}
