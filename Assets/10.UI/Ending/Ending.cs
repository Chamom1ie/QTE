using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Ending : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.instance.PlayMusic("GameClear");
        UIDocument _uiDocoument = GetComponent<UIDocument>();
        var root = _uiDocoument.rootVisualElement;
        VisualElement credit = root.Q<VisualElement>("credit");
        VisualElement clickToMove = root.Q("click-move");
        Label playTime = root.Q<Label>("time-lbl");
        Label attack = root.Q<Label>("attack-lbl");
        Label avoid = root.Q<Label>("avoid-lbl");
        Label dodge = root.Q<Label>("dodge-lbl");
        Button lobbyBtn = root.Q<Button>("lobby-btn");

        attack.text = $"������ Ƚ�� : {InfoManager.instance.ClickCount}";
        dodge.text = $"ȸ�� ��� Ƚ�� : {InfoManager.instance.DodgeCount}";
        avoid.text = $"������ �߻�ü : {InfoManager.instance.AvoidBullet}";
        playTime.text = $"�÷��� �ð� : {InfoManager.instance.PlayTime}";
        clickToMove.RegisterCallback<ClickEvent>(e => SceneManager.LoadScene("Lobby"));
        float first = credit.style.top.value.value;
        DOTween.To(() => first, value => credit.style.top = new StyleLength(Length.Percent(value)), -190f, 10).SetEase(Ease.Linear).OnComplete(() =>
                    DOTween.To(() => credit.style.top.value.value, value => credit.style.top = new StyleLength(Length.Percent(value)), first, 10));
    }
}
