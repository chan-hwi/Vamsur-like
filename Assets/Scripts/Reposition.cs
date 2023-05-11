using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 pos = transform.position;

        float dirX = playerPos.x - pos.x;
        float dirY = playerPos.y - pos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        switch (gameObject.tag)
        {
            case "Ground":
                if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * (dirY > 0 ? 1 : -1) * 40);
                }
                else if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * (dirX > 0 ? 1 : -1) * 40);
                }
                break;
            case "Enemy":
                transform.Translate((playerPos - pos).normalized * 20 + new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0f));
                break;
        }
    }
}
