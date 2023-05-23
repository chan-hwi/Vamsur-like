using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int level = 0;
    public float levelInterval = 10f;
    public float spawnRadius = 35f;
    public SpawnData[] spawnDataArray;

    private float spawnTimer = 0f;

    private void Update()
    {
        if (!GameManager.instance.isAlive) return;

        level = Mathf.FloorToInt(GameManager.instance.gameTime / levelInterval);

        int spawnLevel = Mathf.Min(level, spawnDataArray.Length - 1);
        SpawnData curSpawnData = spawnDataArray[spawnLevel];

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= curSpawnData.spawnInterval)
        {
            spawnTimer = 0f;
            Spawn(curSpawnData);
        }
    }

    private void Spawn(SpawnData data)
    {
        GameObject entity = GameManager.instance.poolManager.Get(0);
        entity.GetComponent<Enemy>().Init(data);

        float spawnAngle = Random.Range(0f, Mathf.PI * 2f) * Mathf.Rad2Deg;

        Vector3 spawnPos = new Vector3(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle), 0f) * spawnRadius;
        entity.transform.position = transform.position + spawnPos;
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnInterval;
    public float health;
    public float speed;
    public int spriteType;
}
