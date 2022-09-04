using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelley : enemy
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite[] sprites; //0: default  1: ball
    Transform plyrTrfm;
    int cd, terrainLayerMask = 1 << 6;
    RaycastHit2D hit;
    bool every2;

    private void Start()
    {
        jumpPower = 15;
        plyrTrfm = Toolbox.playerTrfm;
    }

    void FixedUpdate()
    {
        _FixedUpdate();

        if (cd > 0)
        {
            if (cd == 126)
            {
                if (currentFacing == rightFace) SetVelX(9);
                else SetVelX(-9);
                rb.drag = 0;
            }
            if (cd < 50 && Mathf.Abs(trfm.position.x - plyrTrfm.position.x) > 5)
            {
                SetVelX(0);
                rb.drag = 1;
            } else
            {
                if (currentFacing == rightFace) trfm.Rotate(Vector3.forward * 15);
                else trfm.Rotate(Vector3.forward * 15);
            }
            cd--;
        }

        if (every2) everyTwo();
        every2 = !every2;
    }
    void everyTwo()
    {
        if (Mathf.Abs(trfm.position.x - plyrTrfm.position.x) < 10 && Mathf.Abs(trfm.position.y - plyrTrfm.position.y) < 6)
        {
            hit = Physics2D.Linecast(trfm.position, plyrTrfm.position, terrainLayerMask);

            if (!hit)
            {
                if (trfm.position.x - plyrTrfm.position.x > 0) FaceDir(leftFace);
                else FaceDir(rightFace);
                if (Mathf.Abs(trfm.position.x - plyrTrfm.position.x) < 7)
                {
                    if (cd < 1)
                    {
                        Jump();
                        rend.sprite = sprites[1];
                        cd = 150;
                    }
                }
            }
        }
    }
}
