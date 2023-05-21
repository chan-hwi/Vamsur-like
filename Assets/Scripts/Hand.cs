using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;
    Vector3 posRight = new Vector3(0.35f, -0.15f, 0f);
    Vector3 posRightRev = new Vector3(-0.15f, -0.15f, 0f);
    Quaternion rotationLeft = Quaternion.Euler(0, 0, -35);
    Quaternion rotationLeftRev = Quaternion.Euler(0, 0, -135);

    public void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReversed = player.flipX;

        if (isLeft)
        {
            spriter.flipX = isReversed;
            spriter.sortingOrder = isReversed ? 6 : 4;
            transform.localPosition = isReversed ? posRightRev : posRight;
        } else
        {
            spriter.flipY = isReversed;
            spriter.sortingOrder = isReversed ? 4 : 6;
            transform.localRotation = isReversed ? rotationLeftRev : rotationLeft;
        }
    }
}
