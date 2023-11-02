using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;
using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.ShaderGraph;
using UnityEditor.U2D;
using UnityEditor.Timeline.Actions;
using UnityEngine.Timeline;

public class QTEManager : MonoBehaviour
{
    public static QTEManager instance;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject player;
    [SerializeField] private Volume volume;

    InputActionMap playerMap;
    InputActionMap QTEMap;

    public Action<int> LaserAction;
    BoxCollider2D _coll;
    SpriteRenderer _sr;

    Bloom bloom;
    private void Awake()
    {
        playerMap = _inputReader.GetControl().FindAction("Movement")?.actionMap;
        QTEMap = _inputReader.GetControl().FindAction("SpaceQTE")?.actionMap;
        volume = FindObjectOfType<Volume>();
        if (instance == null) instance = this;
        volume.profile.TryGet<Bloom>(out bloom);
        _coll = player.GetComponent<BoxCollider2D>();
        _sr = player.GetComponent<SpriteRenderer>();
    }
    public void ActionMapToPlayer()
    {
        Debug.Log("PlayerActionMap Enabled");
        playerMap.Enable();
        QTEMap.Disable();

        
        _inputReader.GetControl().Player.SetCallbacks(_inputReader);
        StartCoroutine(ActionMapChanged());
    }

    public void ActionMapToQTE()
    {
        QTEMap.Enable();
        playerMap.Disable();

        _inputReader.GetControl().inQTE.SetCallbacks(_inputReader);
        Debug.Log("QTEActionMap Enabled");
        StartCoroutine(ActionMapChanged());

        StartCoroutine(QTEPattern());
    }

    IEnumerator QTEPattern()
    {
        int count = 11;
        float scale = 0.1f;
        float timetime = 0;
        player.GetComponent<PlayerMovement>().enabled = false;
        SetLights(50, 600);
        while (count > 0)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                --count;
                print($"³²Àº È½¼ö : {count}");
            }
            if(timetime > 1.7f)
            {
                Time.timeScale = 1;
                SetLights(2, 30);
                yield return null;
            }
            else
            {
                timetime += Time.deltaTime;
                Mathf.Lerp(scale, 0.6f, timetime/1.7f);
                Time.timeScale = scale;
                yield return null;
            }
            yield return null;
        }
        player.GetComponent<PlayerMovement>().enabled = true;
        ActionMapToPlayer();
        SetLights(2, 30);
        //yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1;
    }
    void SetLights(int bloomValue, int laserThickness)
    {
        bloom.intensity.value = bloomValue;
        LaserAction?.Invoke(laserThickness);
    }

    IEnumerator ActionMapChanged()
    {
        player.tag = "Untagged";
        _sr.color = Color.white;
        yield return new WaitForSeconds(0.8f);
        player.tag = "Player";
        _sr.color = Color.cyan;
    }
}
    