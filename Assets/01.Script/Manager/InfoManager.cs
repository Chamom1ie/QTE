using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public static InfoManager instance;

    private int avoidBullet;
    private int bossHp;
    private int playTime;
    private int clickCount;
    private int dodgeCount;

    public int BossHp { get => bossHp; set => bossHp = value; }
    public int PlayTime { get => playTime; set => playTime = value; }
    public int ClickCount { get => clickCount; set => clickCount = value; }
    public int DodgeCount { get => dodgeCount; set => dodgeCount = value; }
    public int AvoidBullet { get => avoidBullet; set => avoidBullet = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBossHp(int amount)
    {
        BossHp = amount;
    }

    public void SetPlayTime(int amount)
    {
        PlayTime = amount;
    }

    public void SetClickCount(int amount)
    {
        ClickCount = amount;
    }
    public void SetDodgeCount(int amount)
    {
        DodgeCount = amount;
    }
    public void SetAvoidBullet(int amount)
    {
        AvoidBullet = amount;
    }

    public void SetAllValue(int hp, int playTime, int clickCount, int dodgeCount, int avoidBullet)
    {
        BossHp = hp;
        PlayTime = playTime;
        ClickCount = clickCount;
        DodgeCount = dodgeCount;
        AvoidBullet = avoidBullet;

    }
}
