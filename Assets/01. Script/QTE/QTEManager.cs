using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    InputActionMap playerMap;
    InputActionMap QTEMap;
    private void Awake()
    {
        _inputReader.ActionMapControl += ActionMapChanger;
        playerMap = _inputReader.GetControl().FindAction("Movement")?.actionMap;
        QTEMap = _inputReader.GetControl().FindAction("QTEInput")?.actionMap;

        Debug.Log(playerMap);
        Debug.Log(QTEMap);
    }

    void ActionMapChanger()
    {
        if (playerMap.enabled)
        {
            QTEMap.Enable();
            playerMap.Disable();
            _inputReader.GetControl().inQTE.SetCallbacks(_inputReader);
            Debug.Log("QTEActionMap Enabled");
        }
        else if (QTEMap.enabled)
        {
            playerMap.Enable();
            QTEMap.Disable();
            _inputReader.GetControl().Player.SetCallbacks(_inputReader);
            Debug.Log("PlayerActionMap Enabled");
        }
    }
}
    