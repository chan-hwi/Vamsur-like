using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 2.0f;

    private Vector2 moveDir;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
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
}
