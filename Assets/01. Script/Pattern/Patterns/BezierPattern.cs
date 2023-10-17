using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BezierPattern : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject bulletPrf;
    [SerializeField] private Transform _target;
    [SerializeField] private Bullet[] bullets;
    [SerializeField] private Transform[] _p;

    private List<Vector3> oldPos = new();

    private void OnEnable()
    {
        foreach(Transform item in _p)
        {
            item.position += new Vector3(Random.Range(-2, 2), Random.Range(-1, 2));
        }
        StartCoroutine(DotRoutine());
    }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _p[i] = transform.GetChild(i);
            oldPos.Add(_p[i].position);
        }
    }

    IEnumerator DotRoutine(float duration = 0.4f)
    {
        float time;
        float randSign = (Random.Range(0, 2) == 0) ? 1 : -1;
        Vector3 firstPos = player.transform.position;

        for (time = 0; time < 1; time += Time.fixedDeltaTime / duration)
        {
            Vector3 p4 = Vector3.Lerp(_p[0].position, _p[1].position, time);
            Vector3 p5 = Vector3.Lerp(_p[1].position, _p[2].position, time);
            _target.position = Vector3.Lerp(p4, p5, time);

            GameObject obj = Instantiate(bulletPrf, firstPos + (_target.position * randSign), Quaternion.identity);

            yield return null;  
        }
        for (int i = 0; i < _p.Length; i++)
        {
            _p[i].position = oldPos[i];
        }
        Targetting();
    }

    void Targetting()
    {
        bullets = FindObjectsOfType<Bullet>();
        foreach (Bullet bullet in bullets)
        {
            bullet.SetDir((player.transform.position - bullet.transform.position).normalized);
        }
        gameObject.SetActive(false);
    }

}
