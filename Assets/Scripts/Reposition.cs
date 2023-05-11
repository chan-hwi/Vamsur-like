using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 pos = transform.position;

        float dirX = playerPos.x - pos.x;
        float dirY = playerPos.y - pos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        if (diffX < diffY)
        {
            transform.Translate(Vector3.up * (dirY > 0 ? 1 : -1) * 40);
        }
        else if (diffX > diffY)
        {
            transform.Translate(Vector3.right * (dirX > 0 ? 1 : -1) * 40);
        }
    }
}
