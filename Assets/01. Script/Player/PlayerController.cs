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
    Vector3 dir;
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
        dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        GameObject bullet = Instantiate(playerBulletPrf, transform.position, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().SetDir(dir);
    }
}