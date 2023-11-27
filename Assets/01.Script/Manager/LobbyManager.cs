using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel, helpPanel, helpButton, blackPanel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AudioManager.instance.PlayMusic("Lobby");
        for (int i = 0; i < AudioManager.instance.bossBGMs.Length; i++)
        {
            Sound s = AudioManager.instance.bossBGMs[i];

            if (s == null)
            {
                Debug.Log(name);
                Debug.Log("Sound Not Found");
            }
            else
            {
                print(s.clip.length);
            }
        }
    }

    public void StartBtn()
    {
        StartCoroutine(ChangeScene());
    }

    public void SettingBtn()
    {
        UIController.instance.SetMuted();
        AudioManager.instance.LoadVolume();
        settingPanel.SetActive(true);
        AudioManager.instance.PlaySFX("openPanel");
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        AudioManager.instance.SaveVolumes(UIController.instance._musicSlider.value, UIController.instance._sfxSlider.value);
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
        AudioManager.instance.SaveVolumes(UIController.instance._musicSlider.value, UIController.instance._sfxSlider.value);
        yield return Transition();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
