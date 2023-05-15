using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;
    public float health;
    public float maxHealth;
    public bool isAlive = true;
    public RuntimeAnimatorController[] animCons;

    private Vector2 moveDir;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        moveDir = (GameManager.instance.player.GetComponent<Rigidbody2D>().position - rigid.position).normalized;
        rigid.MovePosition(rigid.position + moveDir * speed * Time.fixedDeltaTime);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spriter.flipX = moveDir.x < 0;
    }

    private void OnEnable()
    {
        health = maxHealth;
        isAlive = true;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCons[data.spriteType];
        maxHealth = data.health;
        health = data.health;
        speed = data.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        isAlive = false;
    }
}
