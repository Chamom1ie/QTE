using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
    ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent("ParticleSystem") as ParticleSystem;
    }

    private void OnEnable()
    {
        particle.Play();
        StartCoroutine(ReleaseObj());
    }

    IEnumerator ReleaseObj()
    {
        yield return new WaitForSeconds(0.2f);
        PoolManager.Release(gameObject);    
    }
    
}
