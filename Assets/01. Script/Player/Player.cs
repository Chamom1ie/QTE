using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    int hp = 10;

    float invincibleTime = 0.6f;
    public void OnHit(int damage)
    {
        hp -= damage;
        StartCoroutine(HitRoutine());
    }

    IEnumerator HitRoutine()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(invincibleTime);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
