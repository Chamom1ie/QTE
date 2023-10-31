using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class QTEManager : MonoBehaviour
{
    public static QTEManager instance;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] GameObject player;
    InputActionMap playerMap;
    InputActionMap QTEMap;
    [SerializeField] private Volume volume;

    Bloom bloom;
    private void Awake()
    {
        playerMap = _inputReader.GetControl().FindAction("Movement")?.actionMap;
        QTEMap = _inputReader.GetControl().FindAction("SpaceQTE")?.actionMap;
        volume = FindObjectOfType<Volume>();
        if (instance == null) instance = this;
        volume.profile.TryGet<Bloom>(out bloom);
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
        int count = 11;
        float scale = 0.1f;
        float timetime = 0;
        player.GetComponent<PlayerMovement>().enabled = false;
        bloom.intensity.value = 50;
        while (count > 0)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                --count;
                print($"³²Àº È½¼ö : {count}");
            }
            if(timetime < 1.7f)
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
        bloom.intensity.value = 2;
        //yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1;
    }
}
    