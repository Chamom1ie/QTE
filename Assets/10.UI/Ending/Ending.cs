using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ending : MonoBehaviour
{
    private void OnEnable()
    {
        UIDocument _uiDocoument = GetComponent<UIDocument>();
        var root = _uiDocoument.rootVisualElement;
        VisualElement credit = root.Q<VisualElement>("credit");

        DOTween.To(() => credit.style.top.value.value, value => credit.style.top = new StyleLength(Length.Percent(value)), -190f, 10)
                         .SetEase(Ease.Linear)
                         .OnComplete(() => EndingManager.instance.ShowLogo());
    }
}
