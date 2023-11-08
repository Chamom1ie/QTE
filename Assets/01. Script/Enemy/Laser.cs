using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] GameObject blueFXPrf;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        QTEManager.instance.LaserAction += SetLaserThick;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.DecreasePlayerHP(3);
        }
        if (collision.CompareTag("PlayerBullet"))
        {
            KillCollision(collision);
        }
    }
    void SetLaserThick(float laserThickness)
    {
        line.material.SetFloat("_LaserThickness", laserThickness);
    }

    void KillCollision(Collider2D coll)
    {
        Transform collTransform = coll.transform;
        PoolManager.Release(collTransform.gameObject);
        PoolManager.Get(blueFXPrf, collTransform.position, Quaternion.identity);
    }
}
