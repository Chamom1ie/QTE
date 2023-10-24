using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Player player;
    LineRenderer line;
    BoxCollider2D coll;

    int segments = 64;
    float radius = 1.0f;
    int hp = 200;
    void Awake()
    {
        player = FindObjectOfType<Player>();
        line = GetComponentInChildren<LineRenderer>();
        coll = GetComponentInChildren<BoxCollider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Laser());
    }

    public async void BurstEnemy(GameObject bulletPrf)
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 dir = player.transform.position - transform.position;
            GameObject bullet = PoolManager.Get(bulletPrf, transform.position, Quaternion.identity);
            bullet.GetComponent<BossBullet>().SetDir(dir);
            await Task.Delay(160);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            hp -= collision.GetComponent<PlayerBullet>().damage;
            PoolManager.Release(collision.gameObject);
        }
    }

    IEnumerator Laser()
    {
        line.useWorldSpace = false;
        float deltaTheta = 2f * Mathf.PI / segments;
        float theta = 0f;
        for (int i = 0; i < segments/2; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            line.SetPosition(1, new Vector3(x * 10, z * 10, 1));
            coll.offset = new Vector2(x * 0.5f, z * 0.5f);
            coll.size = new Vector2(x * 10, 0.1f);
            theta += deltaTheta;
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
}
