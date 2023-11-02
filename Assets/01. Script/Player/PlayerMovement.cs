using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private InputReader _inputReader;
    Vector2 lastDir;
    private Rigidbody2D _rigidbody;
    SpriteRenderer sr;

    Light2D _light;

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
        _light = GetComponentInChildren<Light2D>();

    }

    private void OnEnable()
    {
        _inputReader.MovementEvent += MovementHandle;
        _inputReader.DashEvent += DashHandle;

        DashHandle();
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
        sr.color = Color.white;
         Vector2 lastVel = _rigidbody.velocity;
        _light.intensity = _light.intensity * 3;
        print("무적");
        _coll.enabled = false;

        _rigidbody.velocity = 1.7f * speed * lastDir;
        yield return dashTime;
        sr.color = Color.cyan;
        _rigidbody.velocity = lastVel;

        _coll.enabled = true;
        _light.intensity = _light.intensity / 3;
        _inputReader.MovementEvent += MovementHandle;
    }
}
