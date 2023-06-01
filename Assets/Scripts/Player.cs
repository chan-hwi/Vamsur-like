using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float speed = 3.0f;
    public float health;
    public float maxHealth = 100f;
    public Vector2 inputVec;

    private Rigidbody2D rigid;
    private SpriteRenderer spRenderer;
    private Animator anim;
    private Scanner scanner;
    public Hand[] hands;
    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!GameManager.instance.isAlive) return;

        rigid.MovePosition(rigid.position + inputVec * speed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isAlive) return;

        if (inputVec.x != 0)
        {
            spRenderer.flipX = inputVec.x < 0;
        }
        anim.SetFloat("speed_f", inputVec.magnitude);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isAlive) return;

        health -= 10f * Time.deltaTime;

        if (health < 0)
        {
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            anim.SetTrigger("death_t");
            GameManager.instance.GameOver();
        }
    }
}
