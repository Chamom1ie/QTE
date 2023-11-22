using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

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

    float playTime;

    public GameObject spaceCounterObj;
    public GameObject blackPanelPrf;
    public Player player;
    public Boss boss;

    private void Awake()
    {
        instance = this;
        bossHPBar = transform.Find("Canvas/BossHPBar/Background/Fill").GetComponent<Image>();
        playerHPBar = transform.Find("Canvas/PlayerHPBar/Background/Fill").GetComponent<Image>();
        AudioManager.instance.StartCoroutine(AudioManager.instance.StartBossMusic());
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
            LogAndHandleException(e, "오브젝트의 이름에 (Clone)이 들어가있습니다..", "오브젝트가 없습니다");
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

            Application.Quit();        }
    }

    private void Update()
    {
        playTime += Time.fixedDeltaTime;
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            GameClear();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Pause();
            AudioManager.instance.PlaySFX("openPanel");
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame && UIController.instance.gameObject.activeSelf)
        {
            Resume();
            AudioManager.instance.PlaySFX("closePanel");
        }
    }

    private void Resume()
    {
        UIController.instance.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void Pause()
    {
        UIController.instance.gameObject.SetActive(true);
        Time.timeScale = 0;

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
                Debug.LogError($"파일 열기 중 오류 발생: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning("로그 파일이 존재하지 않습니다.");
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
            Debug.Log("메시지가 메모장에 저장되었습니다.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"메모장 저장 중 오류 발생: {ex.Message}");
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
        AudioManager.instance.musicSource.Stop();
        InfoManager.instance.SetPlayTime((int)MathF.Round(playTime));
        InfoManager.instance.SetAvoidBullet(boss.bulletCount);
        StartCoroutine(SceneChange("Ending"));
    }

    public void GameOver()
    {
        AudioManager.instance.musicSource.Stop();
        InfoManager.instance.SetBossHp(boss.Hp);
        InfoManager.instance.SetPlayTime((int)MathF.Round(playTime));
        StartCoroutine(SceneChange("GameOver"));
        
    }

    IEnumerator SceneChange(string sceneName)
    {
        GameObject obj = Instantiate(blackPanelPrf, Vector3.zero, Quaternion.identity, transform.Find("Canvas"));
        obj.SetActive(true);
        obj.GetComponent<Image>().DOFade(1, 0.5f);
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlaySFX("changeScene");
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(sceneName);
    }
}
