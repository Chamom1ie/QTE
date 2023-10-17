using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private InputReader _inputReader;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _inputReader.MovementEvent += MovementHandle;
    }

    private void OnDestroy()
    {
        _inputReader.MovementEvent -= MovementHandle;
    }

    public void MovementHandle(Vector2 movement)
    {
        _rigidbody.velocity = movement * speed;
    }

}
