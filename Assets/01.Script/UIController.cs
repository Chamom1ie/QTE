using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Slider _musicSlider, _sfxSlider;
    public GameObject _musicMute, _sfxMute;

    private void OnEnable()
    {
        AudioManager.instance.LoadVolume();
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
        _musicMute.SetActive(!_musicMute.activeSelf);
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
        _sfxMute.SetActive(!_sfxMute.activeSelf);
    }

    public void SetMuted()
    {
        _sfxMute.SetActive(AudioManager.instance.sfxSource.mute);
        _musicMute.SetActive(AudioManager.instance.musicSource.mute); 
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(_sfxSlider.value);
    }

    public void SetValue(float musicV, float sfxV)
    {
        _musicSlider.value = musicV;
        _sfxSlider.value = sfxV;
    }
}
