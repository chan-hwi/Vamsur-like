using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        if (index >= pools.Length) return null;

        foreach (GameObject prefab in pools[index])
        {
            if (!prefab.activeSelf)
            {
                prefab.SetActive(true);
                return prefab;
            }
        }

        GameObject newInstance = Instantiate(prefabs[index], transform);
        pools[index].Add(newInstance);
        return newInstance;
    }
}
