using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject bulletPrf;
    Player player;
    int hp = 200;

    float shotgunCount = 10;

    public delegate void ShotArr();
    public ShotArr[] Funcs = new ShotArr[2];

    void Awake()
    {
        player = FindObjectOfType<Player>();
        Funcs[0] = BurstEnemy;
        Funcs[1] = Shotgun;
        //Funcs[2] = SideToMid;
    }

    public async void BurstEnemy()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector2 dir = player.transform.position - transform.position;
            GameObject bullet = PoolManager.Get(bulletPrf, transform.position, Quaternion.identity);
            bullet.GetComponent<BossBullet>().SetDir(dir);
            CamManager.instance.StartShake(4, 0.2f);
            await Task.Delay(160);
        }
    }

    public void Shotgun()
    {
        Vector2 dirMin = player.transform.position - transform.position + Vector3.down * 2 + Vector3.left * 2;
        Vector2 dirMax = player.transform.position - transform.position + Vector3.up * 2 + Vector3.right * 2;
        for (int i = 1; i <= shotgunCount; i++)
        {
            GameObject bullet = PoolManager.Get(bulletPrf, transform.position, Quaternion.identity);
            bullet.GetComponent<BossBullet>().SetDir(Vector2.Lerp(dirMin, dirMax, i / shotgunCount));
            CamManager.instance.StartShake(7, 0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 

        if (collision.CompareTag("PlayerBullet"))
        {
            hp -= collision.GetComponent<PlayerBullet>().damage;
            CamManager.instance.StartShake(2, 0.38f);
            print($"Boss HP : {hp}");
            PoolManager.Release(collision.gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnHit(2);
        }
    }
}
