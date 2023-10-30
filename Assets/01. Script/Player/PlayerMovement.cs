using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private InputReader _inputReader;
    Vector2 lastDir;
    private Rigidbody2D _rigidbody;

    private bool isCooldown = false;
    private float dashCool = 3f;
    private float lastDashTime = 0f;
    WaitForSeconds dashTime= new WaitForSeconds(0.2f);

    BoxCollider2D _coll;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();

        _inputReader.MovementEvent += MovementHandle;
        _inputReader.DashEvent += DashHandle;
    }

    private void OnDestroy()
    {
        _inputReader.MovementEvent -= MovementHandle;
        _inputReader.DashEvent -= DashHandle;
    }
    private void Update()
    {
        print(lastDir);
        if (isCooldown)
        {
            if (Time.time - lastDashTime >= dashCool)
            {
                isCooldown = false;
            }
        }
    }


    public void MovementHandle(Vector2 movement)
    {
        _rigidbody.velocity = movement * speed;
        lastDir = movement;
    }

    public void DashHandle()
    {
        if (!isCooldown)
        {
            StartCoroutine(DashRoutine());
            isCooldown = true;
        }
    }

    IEnumerator DashRoutine()
    {
        _inputReader.MovementEvent -= MovementHandle;
        _coll.enabled = false;
        Vector2 lastVel = _rigidbody.velocity;
        _rigidbody.velocity = 2 * speed * lastDir;
        yield return dashTime;
        _rigidbody.velocity = lastVel;
        _inputReader.MovementEvent += MovementHandle;
        _coll.enabled = true;
    }
}
