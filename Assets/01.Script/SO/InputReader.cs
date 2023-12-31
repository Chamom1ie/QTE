using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Control;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions, IInQTEActions
{
    public event Action<Vector2> MovementEvent;
    public event Action AttackEvent;
    public event Action QTEEvent;
    public event Action DashEvent;  
    public event Action SkillEvent;

    private Control _control;

    public Control GetControl()
    {
        return _control;
    }

    private void OnEnable()
    {
        if (_control == null)
        {
            _control = new Control();
            _control.Player.SetCallbacks(this);
        }
        _control.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(value);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started) AttackEvent?.Invoke();
    }

    public void OnBigboy(InputAction.CallbackContext context)
    {
        if (context.started) SkillEvent?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashEvent?.Invoke();
        }
    }

    public void OnSpaceQTE(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            QTEEvent?.Invoke();
        }
    }

}
