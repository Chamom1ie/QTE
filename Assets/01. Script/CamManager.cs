using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public static CamManager instance;

    [SerializeField] Camera mainCam;
    [SerializeField] CinemachineVirtualCamera vcam;

    private void Awake()
    {
        instance = this;
    }
    

}
