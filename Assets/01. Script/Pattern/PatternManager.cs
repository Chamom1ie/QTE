using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    WaitForSeconds cooldown = new(20f);

    private void Start()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        yield return cooldown;
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }
}
