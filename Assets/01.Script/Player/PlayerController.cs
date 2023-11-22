using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField] GameObject playerBulletPrf;
    [SerializeField] GameObject playerBigboyPrf;
    Vector2 dir;
    Camera cam;

    private float attackCool = 0.4f;
    private float lastAttackTime = 0f;
    private bool isCooldown = false;

    private float bigCool = 8.1f;
    private float lastBigTime = 0f;
    private bool isBigCooldown = false;

    public Action<Vector2> ShootAddforce;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        _inputReader.AttackEvent += Attack;
        _inputReader.SkillEvent += BigBoy;
    }

    private void OnDisable()
    {
        _inputReader.AttackEvent -= Attack;
        _inputReader.SkillEvent -= BigBoy;
    }

    private void Update()
    {
        if (isCooldown)
        {
            if (Time.time - lastAttackTime >= attackCool)
            {
                isCooldown = false;
            }
        }
        if (isBigCooldown)
        {
            if (Time.time - lastBigTime >= bigCool)
            {
                isBigCooldown = false;
            }
        }
    }

    void Attack()
    {
        if (!isCooldown)
        {
            dir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            GameObject bullet = PoolManager.Get(playerBulletPrf, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX("miniShot");
            bullet.GetComponent<PlayerBullet>().SetDir(dir);
            InfoManager.instance.ClickCount++;
            isCooldown = true;
            lastAttackTime = Time.time;
        }
    }

    void BigBoy()
    {
        if (!isBigCooldown)
        {
            InfoManager.instance.ClickCount++;
            dir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            GameObject bullet = PoolManager.Get(playerBigboyPrf, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX("bigShot");
            bullet.GetComponent<PlayerBullet>().SetDir(dir);

            isBigCooldown = true;
            ShootAddforce?.Invoke(dir.normalized);
            lastBigTime = Time.time;
        }
    }
}