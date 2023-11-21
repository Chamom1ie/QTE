using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public GameObject blackPanelPrf;
    public Player player;
    public Boss boss;

    private void Awake()
    {
        instance = this;
        bossHPBar = transform.Find("Canvas/BossHPBar/Background/Fill").GetComponent<Image>();
        playerHPBar = transform.Find("Canvas/PlayerHPBar/Background/Fill").GetComponent<Image>();
    }

    public void FindQTEUI()
    {
        try
        {
            qteSpaceCount = transform.Find("Canvas/SpaceBackground/SpaceCounter").GetComponent<Image>();
            text = transform.Find("Canvas/SpaceBackground/Space!!").GetComponent<TextMeshProUGUI>();
            spaceCounterObj = qteSpaceCount.transform.parent.gameObject;
        }
        catch (Exception e)
        {
            LogAndHandleException(e, "������Ʈ�� �̸��� (Clone)�� ���ֽ��ϴ�..", "������Ʈ�� �����ϴ�");
            qteSpaceCount = transform.Find("Canvas/SpaceBackground(Clone)/SpaceCounter").GetComponent<Image>();
            text = transform.Find("Canvas/SpaceBackground(Clone)/Space!!").GetComponent<TextMeshProUGUI>();
            spaceCounterObj = qteSpaceCount.transform.parent.gameObject;
        }
    }

    private void LogAndHandleException(Exception e, string warningMessage, string errorMessage)
    {
        if (e is MissingComponentException || e is NullReferenceException)
        {
            Debug.LogWarning(warningMessage);

            LogToFile(errorMessage);
        }
        else
        {
            Debug.LogError(errorMessage);
            LogToFile(errorMessage);
            OpenLogFile();

            Application.Quit();
        }
    }

    private void OpenLogFile()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fileName = "errorLog.txt";
        string filePath = Path.Combine(desktopPath, fileName);

        if (File.Exists(filePath))
        {
            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                Debug.LogError($"���� ���� �� ���� �߻�: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning("�α� ������ �������� �ʽ��ϴ�.");
        }
    }

    private void LogToFile(string message)
    {
        string logMessage = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt")} : {message}";
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fileName = "errorLog.txt";
        string filePath = Path.Combine(desktopPath, fileName);

        try
        {
            File.AppendAllText(filePath, logMessage + Environment.NewLine);
            Debug.Log("�޽����� �޸��忡 ����Ǿ����ϴ�.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"�޸��� ���� �� ���� �߻�: {ex.Message}");
        }
    }

    public void DestroyQTEUI()
    {
        Destroy(spaceCounterObj);
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
        seq.Join(qteSpaceCount.transform.DOScale(5, 0.2f).OnComplete(DestroyQTEUI));
    }

    public void GameClear()
    {
        StartCoroutine(SceneChange("Ending"));
    }

    public void GameOver()
    {
        StartCoroutine(SceneChange("GameOver"));
    }

    IEnumerator SceneChange(string sceneName)
    {
        AudioManager.instance.PlaySFX("changeScene");
        GameObject obj = Instantiate(blackPanelPrf);
        obj.GetComponent<Image>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(sceneName);
    }
}
