using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public GameObject _musicMute, _sfxMute;

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

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(_sfxSlider.value);
    }
}
