using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float speed = 3.0f;
    public Vector2 inputVec;

    private Rigidbody2D rigid;
    private SpriteRenderer spRenderer;
    private Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + inputVec * speed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            spRenderer.flipX = inputVec.x < 0;
        }
        anim.SetFloat("speed_f", inputVec.magnitude);
    }
}
