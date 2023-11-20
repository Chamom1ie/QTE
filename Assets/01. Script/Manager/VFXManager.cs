using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FXManager : MonoBehaviour
{
    public static FXManager instance;

    [SerializeField] List<GameObject> effects = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void GetFX(Vector2 pos, Vector2 collPos)
    {
        foreach (GameObject effect in effects)
        {
            PoolManager.Get(effect, new Vector2(Random.Range(pos.x, collPos.x), Random.Range(pos.y, collPos.y)), Quaternion.identity);
        }
    }
}
