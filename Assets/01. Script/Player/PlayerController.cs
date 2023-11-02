using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float bigCool = 0.4f;
    private float lastBigTime = 0f;

    public Action ShootAddforce;

    private void Awake()
    {
        Control control = new Control();
        control.Player.Enable();

        cam = Camera.main;

        _inputReader.AttackEvent += Attack;
        _inputReader.SkillEvent += BigBoy;
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
            dir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            GameObject bullet = Instantiate(playerBigboyPrf, transform.position, Quaternion.identity);
            bullet.GetComponent<PlayerBullet>().SetDir(dir);
            ShootAddforce?.Invoke();

            isCooldown = true;
            lastAttackTime = Time.time;
        }
    }

    
}