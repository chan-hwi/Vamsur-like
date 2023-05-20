using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public Vector3 dir;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;
        this.dir = dir;
        if (per > -1)
        {
            rigid.velocity = dir;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) > 30f)
        {
            gameObject.SetActive(false);
            rigid.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) return;

        per--;
        if (per <= 0)
        {
            gameObject.SetActive(false);
            rigid.velocity = Vector2.zero;
        }
    }
}
