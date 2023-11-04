using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public float bossMaxHP;
    public float playerMaxHp;

    Image bossHPBar;
    Image playerHPBar;
    private void Awake()
    {
        instance = this;
        bossHPBar = transform.Find("BossHPBar/Background/Fill").GetComponent<Image>();
        playerHPBar = transform.Find("PlayerHPBar/Background/Fill").GetComponent<Image>();
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
