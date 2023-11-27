using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] bossBGMs, musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public float musicVolume = 1, sfxVolume = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        //SetPitch();
    }

    private void Start()
    {
        PlayMusic("Lobby");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log(name);
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public IEnumerator StartBossMusic()
    {
        print("StartBossMusic");
        while (true)
        {
            print("다음거");
            string name = "boss" + Random.Range(0, bossBGMs.Length);
            Sound s = Array.Find(bossBGMs, x => x.name == name);
            if (s == null)
            {
                Debug.Log(name);
                Debug.Log("Sound Not Found");
            }
            else
            {
                musicSource.clip = s.clip;
                musicSource.Play();
                yield return new WaitForSeconds(s.clip.length);
            }
            yield return null;
            if (!GameManager.instance.boss.gameObject.activeSelf) break;
        }
        yield return null;
        print("끝남");
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log(name);
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void SaveVolumes(float musicV, float sfxV)
    {
        musicVolume = musicV;
        sfxVolume = sfxV;
    }

    public void LoadVolume()
    {
        UIController.instance.SetValue(musicVolume, sfxVolume);
    }

    public void SetPitch()
    {
        musicSource.pitch = Time.timeScale;
    }
}
