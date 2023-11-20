using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        text.DOFade(0, 0.2f).SetLoops(-1 ,LoopType.Yoyo);
        text.rectTransform.DOScale(1.5f, 0.2f).SetLoops(-1, LoopType.Yoyo);
    }
}
