using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject bulletPrf;
    Player player;
    int hp = 200;

    public delegate void ShotArr();
    public ShotArr[] Funcs = new ShotArr[2];

    void Awake()
    {
        player = FindObjectOfType<Player>();
        Funcs[0] = BurstEnemy;
        Funcs[1] = Shotgun;
    }

    public async void BurstEnemy()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2 dir = player.transform.position - transform.position;
            GameObject bullet = PoolManager.Get(bulletPrf, transform.position, Quaternion.identity);
            bullet.GetComponent<BossBullet>().SetDir(dir);
            await Task.Delay(160);
        }
    }

    public void Shotgun()
    {
        Vector2 dirMin = player.transform.position - transform.position + Vector3.down * 5;
        Vector2 dirMax = player.transform.position - transform.position + Vector3.up * 5;
        for (int i = 1; i <= 8; i++)
        {
            int cnt = 1;
            GameObject bullet = PoolManager.Get(bulletPrf, transform.position, Quaternion.identity);
            bullet.GetComponent<BossBullet>().SetDir(Vector2.Lerp(dirMin, dirMax, cnt / 8));
            ++cnt;

            Debug.Log(dirMin);
            Debug.Log(dirMax);
            Debug.Log(Vector2.Lerp(dirMin, dirMax, i / 8));
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
}
