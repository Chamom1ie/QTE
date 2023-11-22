using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public static CamManager instance;

    float frequency = 5;

    private CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin cameraPerlin;

    private void Awake()
    {
        instance = this;
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
        cameraPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    IEnumerator ShakeCam(float amplitude, float time)
    {
        cameraPerlin.m_FrequencyGain = frequency;
        float currentTime = 0f;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            cameraPerlin.m_AmplitudeGain = Mathf.Lerp(amplitude, 0, currentTime / time);

            yield return null;
        }
        cameraPerlin.m_AmplitudeGain = 0;
    }

    public void StartShake(float amplitude, float time)
    {
        StartCoroutine(ShakeCam(amplitude, time));
    }
}
