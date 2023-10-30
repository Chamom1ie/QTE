using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private InputReader _inputReader;
    Vector2 lastDir;
    private Rigidbody2D _rigidbody;
    SpriteRenderer sr;

    private bool isCooldown = false;
    private float dashCool = 3f;
    private float lastDashTime = 0f;
    WaitForSeconds dashTime= new WaitForSeconds(0.2f);

    BoxCollider2D _coll;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();

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
            isCooldown = true;
            lastDashTime = Time.time;
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    {

        _inputReader.MovementEvent -= MovementHandle;
        Color wasColor = sr.color;
        sr.color = Color.white;
         Vector2 lastVel = _rigidbody.velocity;
        print("¹«Àû");
        _coll.enabled = false;

        _rigidbody.velocity = 1.7f * speed * lastDir;
        yield return dashTime;
        sr.color = wasColor;
        _rigidbody.velocity = lastVel;

        _coll.enabled = true;
        _inputReader.MovementEvent += MovementHandle;
    }
}
