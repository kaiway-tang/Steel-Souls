using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileEntity : HPEntity
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] float knockbackFactor = 1;
    protected Vector2 vect2; protected Vector3 vect3;

    protected bool currentFacing;
    protected int doubleJumps, remainingJumps, lockFacing;
    protected const bool leftFace = true, rightFace = false;
    protected float maxSpeed, xAccl, jumpPower, defaultGravity;

    protected int knockedTmr, noGravityTmr;

    public bool isOnGround;

    private void Start() { _Start(0, 0, 0, 0); }
    protected void _Start(int entityID, float pSpd, float accl, float pJumpPower, int pDoubleJumps = 0)
    {
        _Start(entityID);
        xAccl = accl;
        defaultGravity = rb.gravityScale;
        maxSpeed = pSpd;
        jumpPower = pJumpPower;
        doubleJumps = pDoubleJumps;
    }

    new protected void _FixedUpdate()
    {
        base._FixedUpdate();
        if (knockedTmr > 0)
        {
            knockedTmr--;
            if (knockedTmr < 1) rb.sharedMaterial = Toolbox.defaultMaterial;
        }
        if (noGravityTmr > 0)
        {
            noGravityTmr--;
            if (noGravityTmr < 1) rb.gravityScale = defaultGravity;
        }
    }

    protected void SetRelativeVelX(float val, bool overrideKnocked = false)
    {
        if (IsKnocked() && !overrideKnocked) return;
        if (currentFacing == leftFace) val = -val;
        vect2.x = val; vect2.y = rb.velocity.y;
        rb.velocity = vect2;
    }

    protected void SetVelX(float val, bool overrideKnocked = false)
    {
        if (IsKnocked() && !overrideKnocked) return;
        vect2.x = val; vect2.y = rb.velocity.y;
        rb.velocity = vect2;
    }
    protected void AddVelX(float val, bool overrideKnocked = false)
    {
        if (IsKnocked() && !overrideKnocked) return;
        if (val > 0 && rb.velocity.x < maxSpeed)
        {
            vect2.x += val; vect2.y = rb.velocity.y;
            if (vect2.x > maxSpeed) vect2.x = maxSpeed;
            rb.velocity = vect2;
        } else if (val < 0 && rb.velocity.x > -maxSpeed)
        {
            vect2.x += val; vect2.y = rb.velocity.y;
            if (vect2.x < -maxSpeed) vect2.x = -maxSpeed;
            rb.velocity = vect2;
        }
    }
    protected void SetVelY(float val, bool overrideKnocked = false)
    {
        if (IsKnocked() && !overrideKnocked) return;
        vect2.x = rb.velocity.x; vect2.y = val;
        rb.velocity = vect2;
    }
    protected void FaceDir(bool direction)
    {
        if (currentFacing == direction || lockFacing > 0) return;
        if (direction == leftFace)
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

    protected void Jump(bool overrideKnocked = false)
    {
        if (!IsKnocked() || overrideKnocked)
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
    protected void PseudoJump(float power)
    {
        SetVelY(power);
    }

    protected void ZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    public bool IsKnocked()
    {
        return knockedTmr > 0;
    }
    protected void SetKnocked(int duration) //makes the entity unable to move (use for knockback, knockup, etc) 
    {
        if (knockedTmr < duration)
        {
            knockedTmr = duration;
            rb.sharedMaterial = Toolbox.frictionMaterial;
        }
    }
    public override void knockback(float x, float y, float strength, int ignoreID = -1, int knockedDuration = -1)
    {
        if (entityID == ignoreID) return;
        strength = strength * knockbackFactor;
        vect2.x = x; vect2.y = y;
        rb.velocity = vect2.normalized * strength;
        if (knockedDuration > -1) SetKnocked(knockedDuration);
        else SetKnocked((int)(strength * 1.2f));
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
