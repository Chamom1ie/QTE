using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Control;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField] GameObject playerBulletPrf;
    float bulletSpeed;
    Vector2 dir;
    Camera cam;

    private void Awake()
    {
        Control control = new Control();
        control.Player.Enable();

        cam = Camera.main;

        _inputReader.AttackEvent += Attack;
    }

    void Attack()
    {
        dir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        GameObject bullet = Instantiate(playerBulletPrf, transform.position, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().SetDir(dir);
    }

    
}