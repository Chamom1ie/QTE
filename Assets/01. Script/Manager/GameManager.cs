using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float bossMaxHP;
    public float playerMaxHp;
    public float qteMaxCount;

    Image bossHPBar;
    Image playerHPBar;
    Image qteSpaceCount;

    TextMeshProUGUI text;

    public GameObject spaceCounterObj;
    public Player player;
    public Boss boss;

    private void Awake()
    {
        instance = this;
        bossHPBar = transform.Find("Canvas/BossHPBar/Background/Fill").GetComponent<Image>();
        playerHPBar = transform.Find("Canvas/PlayerHPBar/Background/Fill").GetComponent<Image>();
        qteSpaceCount = transform.Find("Canvas/SpaceBackground/SpaceCounter").GetComponent<Image>();
        text = transform.Find("Canvas/SpaceBackground/Space!!").GetComponent<TextMeshProUGUI>();
    }

    public void DecreaseBossHP(int amount)
    {
        boss.Hp -= amount;
    }

    public void DecreasePlayerHP(int amount)
    {
        player.Hp -= amount;
        player.OnHit();
    }

    public void PlayerHPChange(int amount)
    {
        playerHPBar.fillAmount = amount / playerMaxHp;
    }

    public void BossHPChange(int amount)
    {
        bossHPBar.fillAmount = amount / bossMaxHP;
    }

    public void ChangeSliderValue(int amount)
    {
        qteSpaceCount.fillAmount = amount / qteMaxCount;
    }

    public void FadeQTECounter()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(spaceCounterObj.GetComponent<Image>().DOFade(0, 0.2f));
        seq.Join(qteSpaceCount.DOFade(0, 0.2f));
        seq.Join(spaceCounterObj.transform.DOScale(5, 0.2f));
        seq.Join(qteSpaceCount.transform.DOScale(5, 0.2f));
        seq.AppendCallback(() => Destroy(text.gameObject));
    }
}
