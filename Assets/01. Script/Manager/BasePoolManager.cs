using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class BasePoolManager : MonoBehaviour
{
    [SerializeField] List<GameObject> poolingObjs = new List<GameObject>();
    [SerializeField] List<int> poolCounts = new List<int>();

    private void Awake()
    {
        for(int i = 0; i < poolingObjs.Count; i++)
        {
            for (int j = 0; j < poolCounts[i]; j++)
            {
                GameObject obj = PoolManager.Get(poolingObjs[i]);
                PoolManager.Release(obj);
            }
        }
    }
}
