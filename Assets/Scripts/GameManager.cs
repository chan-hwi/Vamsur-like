using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isAlive = false;
    public float gameTime = 0f;
    public float maxGameTime = 5 * 60f;

    [Header("# Player Info")]
    public int playerId;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public Player player;
    public PoolManager poolManager;
    public LevelUp uiLevelUp;
    public Result uiResults;
    public GameObject enemyCleaner;

    private void Awake()
    {
        instance = this;
    }

    public void GameStart(int playerId)
    {
        isAlive = true;
        this.playerId = playerId;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.PlayBgm(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
        AudioManager.instance.PlayBgm(false);
    }

    IEnumerator GameOverRoutine()
    {
        isAlive = false;

        yield return new WaitForSeconds(0.5f);

        uiResults.gameObject.SetActive(true);
        uiResults.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
        AudioManager.instance.PlayBgm(false);
    }

    IEnumerator GameVictoryRoutine()
    {
        isAlive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResults.gameObject.SetActive(true);
        uiResults.Win();
        Stop();
    }

    private void Update()
    {
        if (!GameManager.instance.isAlive) return;

        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void getExp()
    {
        if (!isAlive) return;

        exp++;
        if (level < nextExp.Length && exp == nextExp[level])
        {
            exp = 0;
            level++;
            uiLevelUp.Show();
            AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        }
    }

    public void Stop()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isAlive = true;
        Time.timeScale = 1;
    }
}
