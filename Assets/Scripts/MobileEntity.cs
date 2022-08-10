using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileEntity : HPEntity
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] float knockbackFactor = 1;
    Vector2 vect2; Vector3 vect3;

    protected bool currentFacing;
    protected int doubleJumps, remainingJumps;
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

    protected void SetVelX(float val)
    {
        vect2.x = val; vect2.y = rb.velocity.y;
        rb.velocity = vect2;
    }
    protected void AddVelX(float val)
    {
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
    protected void SetVelY(float val)
    {
        vect2.x = rb.velocity.x; vect2.y = val;
        rb.velocity = vect2;
    }
    protected void FaceDir(bool direction)
    {
        if (currentFacing == direction) return;
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
    public override void knockback(float x, float y, int strength, int ignoreID = -1)
    {
        if (entityID == ignoreID) return;
        strength = (int)(strength * knockbackFactor);
        vect2.x = x - trfm.position.x;
        vect2.y = y - trfm.position.y;
        vect2.Normalize();
        vect2.y -= .2f;
        rb.velocity = vect2 * -strength;
        SetKnocked((int)(strength*.5f));
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
