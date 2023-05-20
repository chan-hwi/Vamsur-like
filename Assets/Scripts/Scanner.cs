using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public Transform nearestEnemyTransform;
    public RaycastHit2D[] targets;
    public LayerMask EnemyLayermask;
    public float range;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0f, EnemyLayermask);
        GetNearestEnemy();
    }

    void GetNearestEnemy()
    {
        float minVal = range;
        Transform ret = null;
        foreach (RaycastHit2D hit in targets)
        {
            float dist = Vector3.Distance(hit.transform.position, transform.position);
            if (dist < minVal)
            {
                minVal = dist;
                ret = hit.transform;
            }
        }
        nearestEnemyTransform = ret;
    }

}
