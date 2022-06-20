using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroller : enemy
{
    [SerializeField] int spd;
    int flipTmr;
    private void Start()
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
    private void FixedUpdate()
    {
        if (isOnGround)
        {
        }
        else if (flipTmr < 1)
        {
            turn();
            flipTmr = 15;
        }
        if (flipTmr > 0) flipTmr--;
    }
    void turn()
    {
        FaceDir(!currentFacing);
        if (currentFacing) SetVelX(1); else SetVelX(-1);
    }
    public void bumped()
    {
        turn();
    }
}
