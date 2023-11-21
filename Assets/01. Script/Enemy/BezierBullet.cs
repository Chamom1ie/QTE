using UnityEngine;

public class BezierBullet : Bullets
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
            gameObject.tag = "StandBy";
            dir = Vector3.zero;
            _rigid2d.velocity = Vector2.zero;
            PoolManager.Release(gameObject);
            GameManager.instance.DecreasePlayerHP(Damage);
        }
        if (collision.gameObject.CompareTag("BulletBorder"))
        {
            gameObject.tag = "StandBy";
            dir = Vector3.zero;
            _rigid2d.velocity = Vector2.zero;
            PoolManager.Release(gameObject);
        }
    }
}
