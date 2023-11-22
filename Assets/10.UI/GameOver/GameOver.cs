using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.instance.PlayMusic("GameOver");
        UIDocument _uiDocoument = GetComponent<UIDocument>();
        var root = _uiDocoument.rootVisualElement;
        Label lifeTime = root.Q<Label>("lifetime-lbl");
        Label bossHp = root.Q<Label>("hp-lbl");
        Button lobbyBtn = root.Q<Button>("lobby-btn");

        lifeTime.text = $"당신은 {InfoManager.instance.PlayTime} 초 동안 살아남았습니다.";
        bossHp.text = $"남은 보스 체력 : {InfoManager.instance.BossHp}";

        lobbyBtn.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.instance.PlaySFX("changeScene");
            SceneManager.LoadScene("Lobby");
        });
    }
}
