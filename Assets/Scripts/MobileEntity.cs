using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileEntity : HPEntity
{
    protected Transform trfm;
    protected Rigidbody2D rb;
    Vector2 vect2; Vector3 vect3;

    protected int currentFacing, doubleJumps, remainingJumps;
    protected const int leftDir = 1, rightDir = 0;
    protected float speed, jumpPower, defaultGravity;

    protected int knockedTmr, noGravityTmr;

    protected bool isOnGround;

    private void Start()
    {

    }
    protected void _Start(Transform pTrfm, Rigidbody2D prb, float pSpd, float pJumpPower, int pDoubleJumps = 0)
    {
        rb = prb; defaultGravity = rb.gravityScale;
        trfm = pTrfm;
        speed = pSpd;
        jumpPower = pJumpPower;
        doubleJumps = pDoubleJumps;
    }

    protected void _FixedUpdate()
    {
        if (knockedTmr > 0) knockedTmr--;
        if (noGravityTmr > 0)
        {
            noGravityTmr--;
            if (noGravityTmr < 1) rb.gravityScale = defaultGravity;
        }
    }

    protected void SetVelX(float val)
    {
        vect2.x = val; vect2.y = rb.velocity.y;
        rb.velocity = vect2;
    }
    protected void SetVelY(float val)
    {
        vect2.x = rb.velocity.x; vect2.y = val;
        rb.velocity = vect2;
    }
    protected void FaceDir(int direction)
    {
        if (currentFacing == direction) return;
        if (direction == leftDir)
        {
            if (trfm.localScale.x > 0) vect3.x = -trfm.localScale.x;
            else vect3.x = trfm.localScale.x;
        }
        else
        {
            if (trfm.localScale.x < 0) vect3.x = -trfm.localScale.x;
            else vect3.x = trfm.localScale.x;
        }
        currentFacing = direction;
        vect3.y = trfm.localScale.y;
        vect3.z = 1;
        trfm.localScale = vect3;
    }

    protected void Jump()
    {
        if (!IsKnocked())
        {
            if (isOnGround)
            {
                SetVelY(jumpPower);
            }
            else if (remainingJumps > 0) //double jump
            {
                SetVelY(jumpPower);
                remainingJumps--;
            }
        }
    }

    protected void ZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    protected bool IsKnocked()
    {
        return knockedTmr > 0;
    }
    protected void SetKnocked(int duration) //makes the entity unable to move (use for knockback, knockup, etc) 
    {
        if (knockedTmr < duration) knockedTmr = duration;
    }
    protected bool HasGravity()
    {
        return noGravityTmr < 1;
    }
    protected void setNoGravity(int duration)
    {
        if (noGravityTmr < duration)
        {
            noGravityTmr = duration;
            rb.gravityScale = 0;
        }
    }

    public void OnTouchedGround()
    {
        isOnGround = true;
        remainingJumps = doubleJumps;
    }
    public void OnGround()
    {
        isOnGround = true;
        remainingJumps = doubleJumps;
    }
    public void OnLeftGround()
    {
        isOnGround = false;
    }
}
