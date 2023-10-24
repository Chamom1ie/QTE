using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossLaserPattern : MonoBehaviour
{
    [SerializeField] GameObject laser;

    delegate void SeqArr();

    SeqArr[] sequences = new SeqArr[4];

    private void OnEnable()
    {
        sequences[0] = TopSeq;
        sequences[1] = BotSeq;
        sequences[2] = LeftSeq;
        sequences[3] = RightSeq;

        int rand = Random.Range(0, sequences.Length);
        Debug.Log(rand);
        sequences[rand]();
    }

    void TopSeq()
    {
        Sequence seq = DOTween.Sequence();
        int randSign = (Random.Range(0, 2) == 0) ? 1 : -1;
        transform.eulerAngles = Vector3.zero;
         transform.localScale = new Vector2(randSign, 1);
        seq.Append(transform.DOMove(new Vector2(0, 4.5f), 0.5f).OnComplete(
            () =>
            {
            }));
        seq.AppendCallback(() => laser.SetActive(true));
        seq.Append(transform.DORotate(new Vector3(0, 0, 180 * -transform.localScale.x), 0.8f, RotateMode.WorldAxisAdd).SetEase(Ease.InCubic));
        seq.AppendInterval(0.15f);
        seq.AppendCallback(LaserActiveFalse);
    }

    void BotSeq()
    {
        Sequence seq = DOTween.Sequence();
        int randSign = (Random.Range(0, 2) == 0) ? 1 : -1;
        transform.eulerAngles = Vector3.zero;
        transform.localScale = new Vector2(randSign, 1);
        seq.Append(transform.DOMove(new Vector2(0, -4.5f), 0.5f).OnComplete(
            () =>
            {
            }));
        seq.AppendCallback(() => laser.SetActive(true));
        seq.Append(transform.DORotate(new Vector3(0, 0, 180 * transform.localScale.x), 0.8f, RotateMode.WorldAxisAdd).SetEase(Ease.InCubic));
        seq.AppendInterval(0.15f);
        seq.AppendCallback(LaserActiveFalse);
    }

    void LeftSeq()
    {
        Sequence seq = DOTween.Sequence();
        int randSign = (Random.Range(0, 2) == 0) ? 1 : -1;
        transform.eulerAngles = Vector3.zero;
        transform.localScale = new Vector2(randSign, 1);
        seq.Append(transform.DOMove(new Vector2(-8.5f, 0), 0.5f).OnComplete(
            () =>
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
            }));
        seq.AppendCallback(() => laser.SetActive(true));
        seq.Append(transform.DORotate(new Vector3(0, 0, 180 * -transform.localScale.x), 0.8f, RotateMode.WorldAxisAdd).SetEase(Ease.InCubic));
        seq.AppendInterval(0.15f);
        seq.AppendCallback(LaserActiveFalse);
    }

    void RightSeq()
    {
        Sequence seq = DOTween.Sequence();
        int randSign = (Random.Range(0, 2) == 0) ? 1 : -1;
        transform.eulerAngles = Vector3.zero;
        transform.localScale = new Vector2(randSign, 1);
        seq.Append(transform.DOMove(new Vector2(8.5f, 0), 0.5f).OnComplete(
            () =>
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
            }));
        seq.AppendCallback(() => laser.SetActive(true));
        seq.Append(transform.DORotate(new Vector3(0, 0, 180 * transform.localScale.x), 0.8f, RotateMode.WorldAxisAdd).SetEase(Ease.InCubic));
        seq.AppendInterval(0.15f);
        seq.AppendCallback(LaserActiveFalse);
    }

    void LaserActiveFalse()
    {
        laser.SetActive(false);
    }

}
