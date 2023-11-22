using UnityEngine;

public class Bullets : MonoBehaviour
{
    internal Vector3 dir;
    internal Rigidbody2D _rigid2d;
    int damage = 1;

    public int Damage { get => damage; set => damage = value; }

    public virtual void SetDir(Vector3 moveDir)
    {
        dir = moveDir;
    }

    public virtual void CollisionAny(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.DecreasePlayerHP(Damage);
            dir = Vector3.zero;
            _rigid2d.velocity = Vector2.zero;
            PoolManager.Release(gameObject);
        }
        if (collision.CompareTag("BulletBorder"))
        {
            dir = Vector3.zero;
            _rigid2d.velocity = Vector2.zero;
            PoolManager.Release(gameObject);
        }
    }

    
}
