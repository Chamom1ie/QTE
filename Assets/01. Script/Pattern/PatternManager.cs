using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    WaitForSeconds cooldown = new(7f);

    private void Start()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        while (true)
        {
            //transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(true);
            yield return cooldown;
        }
    }
}
