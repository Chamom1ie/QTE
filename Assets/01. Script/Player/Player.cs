using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEditor.ShaderKeywordFilter;

public class Player : MonoBehaviour
{
    public Action PlayerDead;

    [SerializeField] GameObject redFXPrf;  

    int hp = 10;

    readonly float invincibleTime = 0.5f;

    private SpriteRenderer sr;
    private BoxCollider2D coll;

    public int Hp { get => hp; set => hp = value; }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        GameManager.instance.playerMaxHp = Hp;
    }

    public void OnHit()
    {
        if (coll.enabled == false) return;
        coll.enabled = false;
        HitFX();
        GameManager.instance.PlayerHPChange(Hp);
        CamManager.instance.StartShake(7, 0.08f);

        StartCoroutine(TimeScaler());
        StartCoroutine(HitRoutine());

        if (Hp <= 0)
        {
            PlayerDead?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void HitFX()
    {
        PoolManager.Get(redFXPrf, transform.position, Quaternion.identity);
    }

    IEnumerator HitRoutine()
    {
        Tween fadeTween = sr.DOFade(0.2f, 0.15f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        yield return new WaitForSeconds(invincibleTime);
        fadeTween.Kill();
        sr.color = Color.cyan;
        coll.enabled = true;
    }

    IEnumerator TimeScaler()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.03f);
        Time.timeScale = 1;
    }
}
