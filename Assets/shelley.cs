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
    bool every2, isRolling, passedBy;

    private void Start()
    {
        jumpPower = 27;
        plyrTrfm = Toolbox.playerTrfm;
    }

    void FixedUpdate()
    {
        _FixedUpdate();

        if (cd > 0)
        {
            if (cd == 126)
            {
                rb.drag = 0;
            }
            cd--;
        }

        if (isRolling)
        {

            if (currentFacing == rightFace) trfm.Rotate(Vector3.forward * -30);
            else trfm.Rotate(Vector3.forward * 30);

            if (cd < 126)
            {
                SetRelativeVelX(9);

                if ((passedBy && Mathf.Abs(trfm.position.x - plyrTrfm.position.x) > 5) || cd < 1)
                {
                    stopRolling();
                }
            } else
            {
                SetRelativeVelX(5);
            }

            if (Mathf.Abs(trfm.position.x - plyrTrfm.position.x) < 1) passedBy = true;
        }

        if (every2) everyTwo();
        every2 = !every2;
    }
    void everyTwo()
    {
        if (Mathf.Abs(trfm.position.x - plyrTrfm.position.x) < 14 && Mathf.Abs(trfm.position.y - plyrTrfm.position.y) < 6)
        {
            hit = Physics2D.Linecast(trfm.position, plyrTrfm.position, terrainLayerMask);

            if (!hit || (Mathf.Abs(trfm.position.x - plyrTrfm.position.x) < 6 && Mathf.Abs(trfm.position.y - plyrTrfm.position.y) < 6))
            {
                if (!isRolling)
                {
                    if (trfm.position.x - plyrTrfm.position.x > 0) FaceDir(leftFace);
                    else FaceDir(rightFace);
                }

                if (cd < 1)
                {
                    if (Mathf.Abs(trfm.position.x - plyrTrfm.position.x) < 10)
                    {
                        isRolling = true;
                        Jump();
                        rend.sprite = sprites[1];
                        cd = 150;
                    }
                }
            }
        }
    }

    void stopRolling()
    {
        SetVelX(0);
        rb.drag = 1;
        trfm.rotation = Quaternion.identity;
        rend.sprite = sprites[0];
        isRolling = false;
        passedBy = false;
    }
}
