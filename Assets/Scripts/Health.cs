using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
