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
    private Collider2D coll;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator anim;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isAlive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        moveDir = (GameManager.instance.player.GetComponent<Rigidbody2D>().position - rigid.position).normalized;
        rigid.MovePosition(rigid.position + moveDir * speed * Time.fixedDeltaTime);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isAlive) return;

        spriter.flipX = moveDir.x < 0;
    }

    private void OnEnable()
    {
        health = maxHealth;
        isAlive = true;
        rigid.simulated = true;
        coll.enabled = true;
        spriter.sortingOrder = 3;
        anim.SetBool("Dead", false);
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
        if (!collision.CompareTag("Bullet") || !isAlive) return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            anim.SetTrigger("Hit");
            StartCoroutine(Hit());
        }
        else
        {
            isAlive = false;
            Vector3 tmp = rigid.position;
            rigid.simulated = false;
            coll.enabled = false;

            transform.position = tmp;
            spriter.sortingOrder = 2;
            anim.SetBool("Dead", true);

            GameManager.instance.kill++;
            GameManager.instance.GetComponent<GameManager>().getExp();
        }
    }

    IEnumerator Hit()
    {
        yield return new WaitForFixedUpdate();

        Vector3 dir = transform.position - GameManager.instance.player.transform.position;
        rigid.AddForce(dir.normalized * 5f, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
