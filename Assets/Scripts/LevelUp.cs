using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        GameManager.instance.Stop();

        // Disable all items at first
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // Get random values from 0 to item length to use as indices for items to show
        int[] ret = Util.GetDistinctRandomNumbers(0, items.Length, 3);

        // Enable chosen items
        foreach (int idx in ret)
        {
            if (items[idx].level == items[idx].data.damages.Length)
            {
                items[4].gameObject.SetActive(true); // Heal Item
            }
            else
            {
                items[idx].gameObject.SetActive(true);
            }
        }
        rect.localScale = new Vector3(1, 1, 1);

        AudioManager.instance.PlayAudioEffect(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
    }

    public void Hide()
    {
        GameManager.instance.Resume();
        rect.localScale = Vector3.zero;

        AudioManager.instance.PlayAudioEffect(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }
}
