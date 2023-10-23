using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private void Awake()
    {
        _inputReader.ActionMapControl += ActionMapChanger;
    }

    void ActionMapChanger()
    {
        Debug.Log(_inputReader.GetControl().FindAction("Player")?.actionMap);
    }
}
    