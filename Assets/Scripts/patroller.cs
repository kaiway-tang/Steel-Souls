using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroller : enemy
{
    [SerializeField] float spd;
    int flipTmr, perSec;
    bool every2, knocked;
    private new void Start()
    {
        _Start();
    }
    protected void _Start()
    {
        if (Random.Range(0, 2) == 1)
        {
            FaceDir(leftFace);
            SetVelX(-spd);
        }
        else
        {
            SetVelX(spd);
        }
    }
    protected void FixedUpdate()
    {
        _FixedUpdate();
        if (every2) everyTwo();
        every2 = !every2;
    }
    void everyTwo()
    {
        if (knocked != IsKnocked())
        {
            knocked = IsKnocked();
            if (!knocked)
            {
                SetVelFacing();
            }
        }
        if (isOnGround)
        {
        }
        else if (flipTmr < 1)
        {
            turn();
            flipTmr = 5;
        }
        if (flipTmr > 0) flipTmr--;
        if (perSec>0) { perSec--; } else
        {
            perSec = 25;
            ResetVelocity();
        }
    }
    void turn()
    {
        FaceDir(!currentFacing);
        SetVelFacing();
    }
    void SetVelFacing()
    {
        if (!IsKnocked())
        {
            if (currentFacing) SetVelX(-spd); else SetVelX(spd);
        }
    }
    void ResetVelocity()
    {
        if (rb.velocity.x < spd)
        {
            SetVelFacing();
        }
    }
    public void bumped()
    {
        turn();
    }
}
