using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;
    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.type;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    public void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp(rate);
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp(rate);
                break;
        }
    }

    public void RateUp(float rate)
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    weapon.speed = 150f * (1f + rate);
                    break;
                default:
                    weapon.speed = 0.4f / (1f + rate);
                    break;
            }
        }
    }

    public void SpeedUp(float rate)
    {
        GameManager.instance.player.GetComponent<Player>().speed = 3f * (1f + rate);
    }
}
