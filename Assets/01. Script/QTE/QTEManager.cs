using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private void OnEnable()
    {
        _inputReader.ActionMapControl += ActionMapChanger; 
    }

    void ActionMapChanger()
    {
        Debug.Log(21958283);
        _inputReader.GetControl().Player.Disable();
        _inputReader.GetControl().inQTE.Enable();
        _inputReader.QTEEvent += () => Debug.Log(1);
     }

    /*private void QTEEnable()
    {
        playerInput.GetControl().inQTE.Enable();
    }

    public void PlayerEnable()
    {
        playerInput.GetControl().Player.Enable();
    }

    private void QTEDisable()
    {
        playerInput.GetControl().inQTE.Disable();
    }

    private void PlayerDisble()
    {
        playerInput.GetControl().Player.Disable();
    }*/
}
