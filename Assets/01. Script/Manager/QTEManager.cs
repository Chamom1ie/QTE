using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

public class QTEManager : MonoBehaviour
{
    public static QTEManager instance;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GameObject player;
    [SerializeField] private Volume volume;

    InputActionMap playerMap;
    InputActionMap QTEMap;

    public Action<float> LaserAction;
    SpriteRenderer _sr;

    Bloom bloom;
    private void Awake()
    {
        playerMap = _inputReader.GetControl().FindAction("Movement")?.actionMap;
        QTEMap = _inputReader.GetControl().FindAction("SpaceQTE")?.actionMap;
        volume = FindObjectOfType<Volume>();
        if (instance == null) instance = this;
        volume.profile.TryGet<Bloom>(out bloom);
        _sr = player.GetComponent<SpriteRenderer>();
    }
    public void ActionMapToPlayer()
    {
        Debug.Log("PlayerActionMap Enabled");
        StartCoroutine(QTESucsess());
        playerMap.Enable();
        QTEMap.Disable();

        
        _inputReader.GetControl().Player.SetCallbacks(_inputReader);
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
        int count = 11;
        float scale = 0.1f;
        float timetime = 0;
        player.GetComponent<PlayerMovement>().enabled = false;
        SetLights(10, 300);
        while (count > 0)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                --count;
                print($"³²Àº È½¼ö : {count}");
            }
            if(timetime > 1.1f)
            {
                Time.timeScale = 1;
                SetLights(2, 30);
                yield return null;
            }
            else
            {
                timetime += Time.deltaTime;
                Mathf.Lerp(scale, 0.8f, timetime/1.1f);
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
    void SetLights(int bloomValue, float laserThickness)
    {
        bloom.intensity.value = bloomValue;
        LaserAction?.Invoke(laserThickness);
    }

    IEnumerator QTESucsess()
    {
        player.tag = "Untagged";
        _sr.color = Color.white;
        yield return new WaitForSeconds(0.8f);
        player.tag = "Player";
        _sr.color = Color.cyan;
    }
}
    