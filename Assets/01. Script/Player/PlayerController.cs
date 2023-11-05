using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField] GameObject playerBulletPrf;
    [SerializeField] GameObject playerBigboyPrf;
    Vector2 dir;
    Camera cam;

    private bool isCooldown = false;
    private float attackCool = 0.4f;
    private float lastAttackTime = 0f;
    
    private bool isBigCooldown = false;
    private float bigCool = 8.1f;
    private float lastBigTime = 0f;

    public Action<Vector2> ShootAddforce;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        print(1);
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
            GameObject bullet = Instantiate(playerBulletPrf, transform.position, Quaternion.identity);
            bullet.GetComponent<PlayerBullet>().SetDir(dir);

            isCooldown = true;
            lastAttackTime = Time.time;
        }
    }
    
    void BigBoy()
    {
        if (!isBigCooldown)
        {
            print("Å«°Å½ô");
            dir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            GameObject bullet = Instantiate(playerBigboyPrf, transform.position, Quaternion.identity);
            bullet.GetComponent<PlayerBullet>().SetDir(dir);

            isBigCooldown = true;
            ShootAddforce?.Invoke(dir.normalized);
            lastBigTime = Time.time;
        }
    }        
}