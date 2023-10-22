using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    public Action PlayerDead;

    int hp = 10;
    float invincibleTime = 0.5f;

    private SpriteRenderer sr;
    private BoxCollider2D coll;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }
    public void OnHit(int damage)
    {
        if (coll.enabled == false) return;
        coll.enabled = false;
        hp -= damage;
        print("����");
        if (hp <= 0)
        {
            PlayerDead?.Invoke();
            gameObject.SetActive(false);
        }
        StartCoroutine(HitRoutine());
    }

    IEnumerator HitRoutine()
    {
        Tween fadeTween = sr.DOFade(0.2f, 0.15f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        yield return new WaitForSeconds(invincibleTime);

        fadeTween.Kill();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        coll.enabled = true;
    }
}