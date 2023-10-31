using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToBossLine : MonoBehaviour
{
    LineRenderer line;
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform bossTransform;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        line.SetPosition(0, playerTransform.position);
        line.SetPosition(1, bossTransform.position);
    }
}
