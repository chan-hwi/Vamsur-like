using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId = -1;
    public float damage;
    public int count;
    public float speed;

    private float timer = 0f;

    private void Update()
    {
        if (!GameManager.instance.isAlive) return;

        switch (id)
        {
            case 0:
                transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer >= speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            default:
                break;
        }

        if (Input.GetButtonDown("Jump")) LevelUp(5, 1);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        id = data.itemId;
        name = "Weapon " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        PoolManager poolManager = GameManager.instance.poolManager;
        for (int index = 0; index < poolManager.prefabs.Length; index++)
        {
            if (poolManager.prefabs[index] == data.projectile)
            {
                prefabId = index;
                break;
            }
        }

        if (prefabId == -1) throw new System.Exception("Invalid PrefabId");


        // Property Set
        switch (id)
        {
            case 0:
                speed = 150f * Character.WeaponSpeed;
                locateWeapon0();
                break;
            default:
                speed = 0.4f * Character.WeaponRate;
                break;
        }

        // Hand Set
        Hand hand = transform.parent.GetComponent<Player>().hands[(int)data.type];
        hand.gameObject.SetActive(true);
        hand.spriter.sprite = data.hand;

        transform.parent.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void LevelUp(float newDamage, int countDiff)
    {
        this.damage = newDamage;
        this.count += countDiff;

        if (id == 0)
            locateWeapon0();

        transform.parent.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void locateWeapon0()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bulletTransform = null;
            if (i < transform.childCount) bulletTransform = transform.GetChild(i);
            else bulletTransform = GameManager.instance.poolManager.Get(prefabId).transform;

            bulletTransform.parent = transform;
            bulletTransform.localPosition = Vector3.zero;
            bulletTransform.localRotation = Quaternion.identity;

            bulletTransform.Rotate(Vector3.forward * 360 * i / count);
            bulletTransform.Translate(bulletTransform.up * 1.5f, Space.World); // bulletTransform.Translate(new Vector3(0,1) * 1.5f, Space.Self);
            bulletTransform.gameObject.GetComponent<Bullet>().Init(this.damage, -1, Vector3.zero); // -1 for Infinite Penetration
        }
    }

    void Fire()
    {
        Transform target = GetComponentInParent<Scanner>().nearestEnemyTransform;
        if (!target) return;

        Transform bulletTransform = GameManager.instance.poolManager.Get(prefabId).transform;

        Vector3 dir = target.position - transform.position;

        bulletTransform.position = transform.position;

        bulletTransform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bulletTransform.gameObject.GetComponent<Bullet>().Init(this.damage, this.count, dir.normalized * 15f);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
