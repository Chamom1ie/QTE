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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<대충에너미스크립트암바투캄>().피달아(); << 이런식으로 짜는 코드가 좋을수가잇음?
            print("enemyHit");
        }
    }
}