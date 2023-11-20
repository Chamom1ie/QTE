using DG.Tweening;
using System.Collections;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel, helpPanel, helpButton, blackPanel;
    //private void Update()
    //{
    //    if (Mouse.current.leftButton.wasPressedThisFrame)
    //    {
    //        AudioManager.instance.PlaySFX("click");
    //    }
    //}

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartBtn()
    {
        StartCoroutine(ChangeScene());
    }

    public void SettingBtn()
    {
        settingPanel.SetActive(true);
        AudioManager.instance.PlaySFX("openPanel");
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        AudioManager.instance.PlaySFX("closePanel");
    }

    public void QuitBtn()
    {
        Application.Quit();
        AudioManager.instance.PlaySFX("QuitGame");
    }

    public void HelpBtn()
    {
        helpPanel.SetActive(true);
        helpButton.SetActive(false);
        AudioManager.instance.PlaySFX("openPanel");
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
        helpButton.SetActive(true);
        AudioManager.instance.PlaySFX("closePanel");
    }

    IEnumerator ChangeScene()
    {
        AudioManager.instance.musicSource.Stop();
        yield return Transition();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.instance.PlayMusic("boss" + Random.Range(1, 3));
        Destroy(gameObject);
    }

    IEnumerator Transition()
    {
        AudioManager.instance.PlaySFX("changeScene");
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Image>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
    }
}
