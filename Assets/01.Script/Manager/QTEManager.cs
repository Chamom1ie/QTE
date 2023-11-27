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
    [SerializeField] private GameObject qteUIprf;
    [SerializeField] private Volume volume;
    [SerializeField] private Canvas canvas;


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

        StartCoroutine(SpaceQTE());
    }

    IEnumerator SpaceQTE()
    {
        int count = 10;
        float scale = 0.1f;
        float timetime = 0;
        PoolManager.Get(qteUIprf, canvas.transform);
        GameManager.instance.FindQTEUI();
        GameManager.instance.qteMaxCount = count;
        GameManager.instance.ChangeSliderValue(10 - count);
        GameManager.instance.spaceCounterObj.SetActive(true);
        player.GetComponent<PlayerMovement>().enabled = false;
        SetLights(10, 300);
        while (count > 0 && player != null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                --count;
                AudioManager.instance.PlaySFX("click");
                GameManager.instance.ChangeSliderValue(10 - count);
                print($"³²Àº È½¼ö : {count}");
            }
            if (timetime > 1.1f)
            {
                Time.timeScale = 1;
                SetLights(2, 30);
                yield return null;
                ActionMapToPlayer();
                player.GetComponent<PlayerMovement>().enabled = true;
                break;
            }
            else
            {
                timetime += Time.deltaTime;
                Mathf.Lerp(scale, 0.8f, timetime / 1f);
                Time.timeScale = scale;
                yield return null;
            }
            yield return null;
        }
        player.GetComponent<PlayerMovement>().enabled = true;
        GameManager.instance.DestroyQTEUI();
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
        GameManager.instance.FadeQTECounter();
        _sr.color = Color.white;
        yield return new WaitForSeconds(0.8f);
        player.tag = "Player";
        _sr.color = Color.cyan;
    }
}
