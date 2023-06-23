using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] LockedCharacters;
    public GameObject[] UnlockedCharacters;
    public GameObject uiNotice;

    enum Achive { PotatoCharacter, BeanCharacter };
    private Achive[] achives;

    private WaitForSecondsRealtime transitionInterval = new WaitForSecondsRealtime(0.0005f);
    private WaitForSecondsRealtime showDuration = new WaitForSecondsRealtime(3f);

    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));

        if (!PlayerPrefs.HasKey("IsInitialized")) Init();
    }

    private void Start()
    {
        UnlockCharacters();
    }

    void UnlockCharacters()
    {
        for (int index = 0; index < LockedCharacters.Length; index++)
        {
            bool isUnlocked = PlayerPrefs.GetInt(achives[index].ToString()) == 1;
            LockedCharacters[index].SetActive(!isUnlocked);
            UnlockedCharacters[index].SetActive(isUnlocked);
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("IsInitialized", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    private void LateUpdate()
    {
        foreach (Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchived = false;

        switch (achive)
        {
            case Achive.PotatoCharacter:
                isAchived = GameManager.instance.kill >= 10;
                break;

            case Achive.BeanCharacter:
                isAchived = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if (isAchived && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            for (int index = 0; index < achives.Length; index++)
            {
                uiNotice.transform.GetChild(index).gameObject.SetActive(index == (int)achive);
            }
            StartCoroutine(NoticeRoutine());
            PlayerPrefs.SetInt(achive.ToString(), 1);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        }
    }

    IEnumerator NoticeRoutine()
    {
        RectTransform rect = uiNotice.GetComponent<RectTransform>();

        uiNotice.SetActive(true);
        for (int i = 200; i >= 0; i--)
        {
            rect.anchoredPosition = new Vector2(i, rect.anchoredPosition.y);
            yield return transitionInterval;
        }

        yield return showDuration;


        for (int i = 0; i <= 200; i++)
        {
            rect.anchoredPosition = new Vector2(i, rect.anchoredPosition.y);
            yield return transitionInterval;
        }
        uiNotice.SetActive(false);
    }
}
