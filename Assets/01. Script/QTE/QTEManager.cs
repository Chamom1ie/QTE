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
            Debug.Log("플레이어뒤짐");
        }
        else if (QTEMap.enabled)
        {
            playerMap.Enable();
            QTEMap.Disable();
        }
    }
}
    