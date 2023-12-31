using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private InputReader _inputReader;
    Vector2 lastDir;
    private Rigidbody2D _rigidbody;
    private PlayerController controller;

    SpriteRenderer sr;

    Light2D _light;

    private bool isCooldown = false;
    private float dashCool = 3f;
    private float lastDashTime = 0f;
    WaitForSeconds dashTime = new WaitForSeconds(0.2f);

    BoxCollider2D _coll;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _coll = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        _light = GetComponentInChildren<Light2D>();
        controller = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        ActionSub();
    }

    private void OnDestroy()
    {
        ActionUnsub();
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
            InfoManager.instance.DodgeCount++;
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    {
        _inputReader.MovementEvent -= MovementHandle;
        sr.color = Color.white;
        Vector2 lastVel = _rigidbody.velocity;
        _light.intensity = _light.intensity * 3;
        _coll.enabled = false;

        _rigidbody.velocity = 1.7f * speed * lastDir;
        AudioManager.instance.PlaySFX("dash");
        yield return dashTime;
        sr.color = Color.cyan;
        _rigidbody.velocity = lastVel;

        _coll.enabled = true;
        _light.intensity = _light.intensity / 3;
        _inputReader.MovementEvent += MovementHandle;
    }

    void AddForceBack(Vector2 dir)
    {
        StartCoroutine(AddForceRoutine(dir));
    }

    IEnumerator AddForceRoutine(Vector2 dir)
    {
        ActionUnsub();
        Vector2 velo = _rigidbody.velocity;
        _rigidbody.AddForce(-dir.normalized * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.175f);
        ActionSub();
        _rigidbody.velocity = velo;
    }

    void ActionSub()
    {
        _inputReader.MovementEvent += MovementHandle;
        _inputReader.DashEvent += DashHandle;
        controller.ShootAddforce += AddForceBack;
    }

    void ActionUnsub()
    {
        _inputReader.MovementEvent -= MovementHandle;
        _inputReader.DashEvent -= DashHandle;
        controller.ShootAddforce -= AddForceBack;
    }
}
