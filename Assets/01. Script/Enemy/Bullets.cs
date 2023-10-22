using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    internal Vector3 dir;
    public int damage = 1;

    public virtual void SetDir(Vector3 moveDir)
    {
        dir = moveDir;
    }

    public virtual void CollisionAny(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().OnHit(damage);
            PoolManager.Release(gameObject);
        }
        if (collision.CompareTag("BulletBorder"))
        {
            PoolManager.Release(gameObject);
        }
    }
}
