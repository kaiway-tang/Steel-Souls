using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpMech : enemy
{
    public int cd, giveUpCheck;
    bool every2, locked, checkedLastPos = true, checkOnGround;
    float lastPlyrX;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        jumpPower = 28;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cd > 0)
        {
            if (cd == 10 && !isOnGround) SetRelativeVelX(9);
            cd--;
        }
        _FixedUpdate();

        if (every2) everyTwo();
        every2 = !every2;
    }

    void everyTwo()
    {

        if ((Toolbox.inBoxRange(trfm.position, plyrTrfm.position, 10) || locked) && (plyrInSight() || Toolbox.inBoxRange(trfm.position, plyrTrfm.position, 5)))
        {
            if (!locked)
            {
                locked = true;
                checkedLastPos = false;
            }
            facePlayerLR();
            if (cd < 1) doJump();
        } else
        {
            if (locked)
            {
                locked = false;
                checkedLastPos = false;
                lastPlyrX = plyrTrfm.position.x;
            }
            if (!checkedLastPos)
            {
                if (Mathf.Abs(trfm.position.x - lastPlyrX) < 2)
                {
                    checkedLastPos = true;
                } else
                {
                    if (cd < 1) doJump();
                }
            }
        }
        if (checkOnGround != isOnGround)
        {
            checkOnGround = isOnGround;
            if (isOnGround) SetVelX(0);
        }
    }

    void doJump()
    {
        cd = Random.Range(20,30);
        Jump();

        SetRelativeVelX(9);
    }
}
