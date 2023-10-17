using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int hp = 10;
    float invincibleTime = 2f;

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
            print("�ֱ�");
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
