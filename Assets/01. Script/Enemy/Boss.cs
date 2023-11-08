using System.Threading.Tasks;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject bulletPrf;
    [SerializeField] GameObject blueFXPrf;
    Player player;
    int hp = 200;

    public int Hp { get => hp; set => hp = value; }

    readonly int damage = 2;
    readonly float shotgunCount = 10;

    public delegate void ShotArr();
    public ShotArr[] Funcs = new ShotArr[2];


    void Awake()
    {
        player = FindObjectOfType<Player>();
        Funcs[0] = BurstEnemy;
        Funcs[1] = Shotgun;
        //Funcs[2] = SideToMid;
    }

    private void Start()
    {
        GameManager.instance.bossMaxHP = Hp;
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

    private void HitFX()
    {
        PoolManager.Get(blueFXPrf, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PlayerBullet"))
        {
            GameManager.instance.BossHPChange(hp);
            HitFX();
            CamManager.instance.StartShake(2, 0.38f);
            if (Hp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else if (collision.CompareTag("Player"))
        {
            GameManager.instance.DecreasePlayerHP(damage);
            HitFX();
        }
    }
}
