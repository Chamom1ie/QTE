using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField] GameObject playerBulletPrf;
    Vector2 dir;
    Camera cam;

    private bool isCooldown = false;
    private float attackCool = 0.4f;
    private float lastAttackTime = 0f;

    private void Awake()
    {
        Control control = new Control();
        control.Player.Enable();

        cam = Camera.main;

        _inputReader.AttackEvent += Attack;
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
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            QTEManager.instance.ActionMapToQTE();
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            QTEManager.instance.ActionMapToPlayer();
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

    
}