using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearBullet : Bullets
{
    Rigidbody2D _rigid2d;
    float speed = 20f;
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
    }
}
