using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class PatternManager : MonoBehaviour
{
    IEnumerator[] patterns;
    [SerializeField] Transform BossController;
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
    [SerializeField] GameObject boss;
    SpriteRenderer sr;
    private Color firstColor;
    #endregion

    [Header("Linear Pattern")]
    #region Linear
    [SerializeField] float radius;
    [SerializeField] List<GameObject> linearBullet = new();
    [SerializeField] GameObject linearBulletPrf;
    #endregion
    int QTECount = 0;

    WaitForSeconds cooldown = new(3);
    private void Awake()
    {
        boss = FindObjectOfType<Boss>().gameObject;
        sr = boss.GetComponent<SpriteRenderer>();

        firstColor = sr.color;
        patterns = new IEnumerator[] { BezierPattern(), DashPattern(), CrossPattern(), CirclePattern() };

        for (int i = 0; i < _p.Length; i++)
        {
            oldPos.Add(_p[i].position);
        }
    }

    private void OnEnable()
    {
        player.PlayerDead += () => gameObject.SetActive(false);
        player.PlayerDead += () => Time.timeScale = 1;
    }

    private void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        ++QTECount;

        //randPattern 개의 패턴 실행
        int randPattern = Random.Range(2, patterns.Length);
        for (int i = 0; i < randPattern; i++)
        {
            patterns = new IEnumerator[] { BezierPattern(), DashPattern(), CirclePattern(), CrossPattern() };
            int random = Random.Range(0, 3);
            yield return StartCoroutine(patterns[random]);
        }
        yield return new WaitForSeconds(0.4f);

        int randLaser = Random.Range(0, 4);
        BossController.GetComponent<BossLaserPattern>().sequences[randLaser]();

        yield return new WaitForSeconds(0.5f);
        if (QTECount >= 3 && Random.Range(0, 5) >= 0)
        {
            QTEManager.instance.ActionMapToQTE();
            QTECount = 0;
        }
        yield return cooldown;
        if (player.gameObject.activeSelf && boss.activeSelf)
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator BezierPattern()
    {
        float duration = 0.2f;
        float time;
        float randSign = (Random.Range(0, 2) == 0) ? 1 : -1;

        foreach (Transform item in _p)
        {
            item.position += new Vector3(Random.Range(-2, 2), Random.Range(-1, 2));
        }

        Vector3 firstPos = player.transform.position;
        for (time = 0; time < 1; time += Time.fixedDeltaTime / duration)
        {
            boss.GetComponent<Boss>().bulletCount++;
            Vector3 p4 = Vector3.Lerp(_p[0].position, _p[1].position, time);
            Vector3 p5 = Vector3.Lerp(_p[1].position, _p[2].position, time);
            _target.position = Vector3.Lerp(p4, p5, time);

            GameObject obj = PoolManager.Get(enemyBullet, firstPos + (_target.position * randSign), Quaternion.identity);
            AudioManager.instance.PlaySFX("makeBullet");

            yield return new WaitForSeconds(0.02f);

        }
        for (int i = 0; i < _p.Length; i++)
        {
            _p[i].position = oldPos[i];
        }
        Targetting();
        yield return new WaitForSeconds(1.2f);
    }
    void Targetting()
    {
        Array.Clear(bullets, 0, bullets.Length);
        bullets = FindObjectsOfType<BezierBullet>();
        CamManager.instance.StartShake(30, 0.25f);
        foreach (BezierBullet bullet in bullets)
        {
            if (bullet.gameObject.tag == "StandBy")
            {
                AudioManager.instance.PlaySFX("shootBullet");
                bullet.SetDir((player.transform.position - bullet.transform.position).normalized);
            }
        }
    }

    IEnumerator DashPattern()
    {
        int rand = Random.Range(2, 5);

        for (int i = 0; i < rand; i++)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(sr.DOColor(bossRed, 0.65f));
            seq.AppendCallback(() =>
            {
                CamManager.instance.StartShake(5, 0.2f);
                AudioManager.instance.PlaySFX("bossDash");
            });
            seq.Append(BossController.DOMove(player.transform.position + new Vector3(Random.Range(-2, 3), Random.Range(-1, 2)), 0.6f));
            seq.AppendCallback(() =>
            {
                var bossFuncs = boss.GetComponent<Boss>().Funcs;
                bossFuncs[Random.Range(0, bossFuncs.Length)]();
                sr.color = firstColor;
            });

            yield return new WaitForSeconds(1.35f);
        }
        yield return new WaitForSeconds(2.5f);
    }

    IEnumerator CrossPattern()
    {
        yield return null;
    }

    IEnumerator CirclePattern()
    {
        linearBullet.Clear();
        float duration = 0.2f;
        Vector2 firstPlayerPos = player.transform.position;
        for (float time = 0; time < 1; time += Time.fixedDeltaTime / duration)
        {
            boss.GetComponent<Boss>().bulletCount++;
            Vector2 randPos = RandomPoint(firstPlayerPos);
            GameObject obj = Instantiate(linearBulletPrf, randPos, Quaternion.identity);
            AudioManager.instance.PlaySFX("makeBullet");
            linearBullet.Add(obj);

            yield return null;
        }
        CamManager.instance.StartShake(6, 0.1f);
        foreach (GameObject obj in linearBullet)
        {
            obj.GetComponent<LinearBullet>().SetDir(player.transform.position - obj.transform.position, Random.Range(0, 4));
            AudioManager.instance.PlaySFX("shootBullet");
        }
        yield return new WaitForSeconds(2.75f);
    }

    Vector2 RandomPoint(Vector2 playerPos)
    {
        float randAngle = Random.Range(0f, 2f * Mathf.PI);

        float x = radius * Mathf.Cos(randAngle);
        float y = radius * Mathf.Sin(randAngle);

        return new Vector2(x, y) + playerPos;
    }
}
