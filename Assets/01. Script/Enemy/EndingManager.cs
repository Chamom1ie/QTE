using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public static EndingManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject toolkitObj, logoObj;

    public void ShowLogo()
    {
        Destroy(toolkitObj);
        logoObj.SetActive(true);
    }
}
