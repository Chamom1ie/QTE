using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float bossMaxHP;
    public float playerMaxHp;

    Image bossHPBar;
    Image playerHPBar;
    public Player player;
    public Boss boss;

    private void Awake()
    {
        instance = this;
        bossHPBar = transform.Find("Canvas/BossHPBar/Background/Fill").GetComponent<Image>();
        playerHPBar = transform.Find("Canvas/PlayerHPBar/Background/Fill").GetComponent<Image>();
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
}
