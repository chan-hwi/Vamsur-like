using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level = 0;
    public Weapon weapon;
    public Gear gear;
    public bool noLevel = false;

    Image itemIcon;
    Text levelText;
    Text nameText;
    Text descText;

    private void Awake()
    {
        itemIcon = GetComponentsInChildren<Image>()[1];

        Text[] texts = GetComponentsInChildren<Text>();
        levelText = texts[0];
        nameText = texts[1];
        descText = texts[2];

        nameText.text = data.itemName;
    }

    private void OnEnable()
    {
        itemIcon.sprite = data.itemIcon;
        if (!noLevel) levelText.text = "LV." + (level + 1);
        else levelText.text = "-";

        switch (data.type)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                descText.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                descText.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;

            default:
                descText.text = string.Format(data.itemDesc);
                break;
        }
    }

    public void OnClick()
    {
        switch (data.type)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject obj = new GameObject();
                    weapon = obj.AddComponent<Weapon>();

                    weapon.Init(data);
                } else
                {
                    float newDamage = data.baseDamage * (1f + data.damages[level]);
                    weapon.LevelUp(newDamage, data.counts[level]);
                }
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject obj = new GameObject();
                    gear = obj.AddComponent<Gear>();

                    gear.Init(data);
                }
                else
                {
                    float newRate = data.damages[level];
                    gear.LevelUp(newRate);
                }
                break;
            case ItemData.ItemType.Heal:
                Player player = GameManager.instance.player;
                player.health = player.maxHealth;
                break;
        }

        if (!noLevel) level++;
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
