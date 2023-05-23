using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isAlive = true;
    public float gameTime = 0f;
    public float maxGameTime = 5 * 60f;

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public Player player;
    public PoolManager poolManager;
    public LevelUp uiLevelUp;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiLevelUp.Show();
    }

    private void Update()
    {
        if (!GameManager.instance.isAlive) return;

        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void getExp()
    {
        exp++;
        if (level < nextExp.Length && exp == nextExp[level])
        {
            exp = 0;
            level++;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isAlive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isAlive = true;
        Time.timeScale = 1;
    }
}
