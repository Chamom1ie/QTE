using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTEManager : MonoBehaviour
{
    public static QTEManager instance;
    [SerializeField] private InputReader _inputReader;
    InputActionMap playerMap;
    InputActionMap QTEMap;
    private void Awake()
    {
        playerMap = _inputReader.GetControl().FindAction("Movement")?.actionMap;
        QTEMap = _inputReader.GetControl().FindAction("QTEInput")?.actionMap;

        if (instance == null) instance = this;

    }

    public void ActionMapToPlayer()
    {
        playerMap.Enable();
        QTEMap.Disable();

        _inputReader.GetControl().Player.SetCallbacks(_inputReader);
        Debug.Log("PlayerActionMap Enabled");
    }

    public void ActionMapToQTE()
    {
        QTEMap.Enable();
        playerMap.Disable();

        _inputReader.GetControl().inQTE.SetCallbacks(_inputReader);
        Debug.Log("QTEActionMap Enabled");

        StartCoroutine(QTEPattern());
    }

    IEnumerator QTEPattern()
    {
        yield return null;
    }
}
    