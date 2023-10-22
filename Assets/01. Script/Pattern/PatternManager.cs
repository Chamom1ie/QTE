using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class PatternManager : MonoBehaviour
{
    IEnumerator[] patterns;

    [Header("Bezier Pattern")]
    #region Bezier
    [SerializeField] private Player player;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform _target;
    [SerializeField] private BezierBullet[] bullets;
    [SerializeField] private Transform[] _p;

    private List<Vector3> oldPos = new();
    #endregion

    [Header("Dash Pattern")]
    #region Dash
    [SerializeField] Color bossRed;
    [SerializeField] GameObject bossBullet;
    GameObject boss;
    SpriteRenderer sr;
    private Color firstColor;
    #endregion

    WaitForSeconds cooldown = new(10);
    float patternDelay = 1;
    private void Awake()
    {
        boss = FindObjectOfType<Boss>().gameObject;
        sr = boss.GetComponent<SpriteRenderer>();
        
        firstColor = sr.color;
        patterns = new IEnumerator[] { BezierPattern(), DashPattern(), CrossPattern(), TriGraphPattern(), AADASD() };

        for (int i = 0; i < _p.Length; i++)
        {
            oldPos.Add(_p[i].position);
        }
    }

    private void OnEnable()
    {
        player.PlayerDead += () => gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (player.gameObject.activeSelf == true) // 들어옴
        {
            // 패턴 몇 개 할까요?
            int rand = Random.Range(2, patterns.Length);
            for (int i = 0; i < 5; i++) //뭐할까요 아래
            {
                patterns = new IEnumerator[] { BezierPattern(), DashPattern(), CrossPattern(), TriGraphPattern(), AADASD() };
                print("patterngogo");
                int random = Random.Range(0, 2);
                StartCoroutine(patterns[random]);
                yield return new WaitForSeconds(patternDelay);
            }
            yield return cooldown;
        }
    }

    IEnumerator BezierPattern()
    {
        print("BezierPattern");
        patternDelay = 1.2f;
        float duration = 0.4f;
        float time;
        float randSign = (Random.Range(0, 2) == 0) ? 1 : -1;

        foreach (Transform item in _p)
        {
            item.position += new Vector3(Random.Range(-2, 2), Random.Range(-1, 2));
        }

        Vector3 firstPos = player.transform.position;
        for (time = 0; time < 1; time += Time.fixedDeltaTime / duration)
        {
            Vector3 p4 = Vector3.Lerp(_p[0].position, _p[1].position, time);
            Vector3 p5 = Vector3.Lerp(_p[1].position, _p[2].position, time);
            _target.position = Vector3.Lerp(p4, p5, time);

            GameObject obj = Instantiate(enemyBullet, firstPos + (_target.position * randSign), Quaternion.identity);

            yield return null;

        }
        for (int i = 0; i < _p.Length; i++)
        {
            _p[i].position = oldPos[i];
        }
        Targetting();
        StopCoroutine(patterns[0]);
    }
    void Targetting()
    {
        print("타 타 타게팅");
        Array.Clear(bullets, 0, bullets.Length); 
        bullets = FindObjectsOfType<BezierBullet>();
        foreach (BezierBullet bullet in bullets)
        {
            if(bullet.gameObject.tag == "StandBy") 
                bullet.SetDir((player.transform.position - bullet.transform.position).normalized);
        }
    }

    IEnumerator DashPattern()
    {
        int rand = Random.Range(2, 5);
        patternDelay = 2 * rand;
        print("DashPattern");

        for (int i = 0; i < rand; i++)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(sr.DOColor(bossRed, 0.65f));
            seq.Append(boss.transform.DOMove(player.transform.position + new Vector3(Random.Range(-2, 3), Random.Range(-1, 2)), 0.6f));
            seq.AppendCallback(() =>
            {
                boss.GetComponent<Boss>().BurstEnemy(bossBullet);
                sr.color = firstColor;
            });

            yield return new WaitForSeconds(1.35f);
        }
    }

    IEnumerator CrossPattern()
    {
        yield return null;
    }

    IEnumerator TriGraphPattern()
    {
        yield return null;
    }

    IEnumerator AADASD()
    {
        yield return null;
    }


}
