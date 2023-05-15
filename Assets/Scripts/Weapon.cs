using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        if (Input.GetButtonDown("Jump")) LevelUp(10, 2);
    }

    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150f;
                locateWeapon0();
                break;
            default:
                break;
        }
    }

    void LevelUp(float damageDiff, int countDiff)
    {
        this.damage += damageDiff;
        this.count += countDiff;

        if (id == 0)
            locateWeapon0();
    }

    void locateWeapon0()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bulletTransform = null;
            if (i < transform.childCount) bulletTransform = transform.GetChild(i);
            else bulletTransform = GameManager.instance.poolManager.GetComponent<PoolManager>().Get(prefabId).transform;

            bulletTransform.parent = transform;
            bulletTransform.localPosition = Vector3.zero;
            bulletTransform.localRotation = Quaternion.identity;

            bulletTransform.Rotate(Vector3.forward * 360 * i / count);
            bulletTransform.Translate(bulletTransform.up * 1.5f, Space.World);
            bulletTransform.gameObject.GetComponent<Bullet>().Init(this.damage, -1); // -1 for Infinite Penetration
        }
    }
}