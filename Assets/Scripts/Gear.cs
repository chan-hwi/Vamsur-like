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
                    float weaponSpeed = 150f * Character.WeaponSpeed;
                    weapon.speed = weaponSpeed * (1f + rate);
                    break;
                default:
                    float weaponRate = 0.4f * Character.WeaponRate;
                    weapon.speed = weaponRate / (1f + rate);
                    break;
            }
        }
    }

    public void SpeedUp(float rate)
    {
        GameManager.instance.player.speed = 3f * (1f + rate);
    }
}
