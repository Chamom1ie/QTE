    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rigid2d;
    Vector3 dir;
    float speed = 5;
    private void Awake()
    {
        _rigid2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);
    }

    void Update()
    {
        _rigid2d.velocity = dir.normalized * speed;
    }

    public void SetDir(Vector3 moveDir)
    {
        dir = moveDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<대충에너미스크립트암바투캄>().피달아(); << 이런식으로 짜는 코드가 좋을수가잇음?
            print("playerHit");
        }
    }
}
