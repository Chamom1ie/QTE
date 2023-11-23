using UnityEngine;

public class SettingCanvas : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = null;
        canvas.worldCamera = Camera.main;
    }
}
