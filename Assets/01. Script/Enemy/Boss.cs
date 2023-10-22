using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Player player;
    int hp = 200;
    void Awake()
    {
        player = FindObjectOfType<Player>();
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
            print(123123);
        }
    }
}
