using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MobileEntity
{
    [SerializeField] bool sameJumpAndUp;
    [SerializeField] float spd, jumpPwr;
    public static KeyCode jumpKey, upKey, downKey, leftKey, rightKey;
    public static KeyCode basicKey, mobilityKey, ultimateKey, specialKey;

    [SerializeField] ManualAnimator slashAnim;
    [SerializeField] Animator playerAnimator;
    int defaultState, overrideState;
    const int idle = 0, walking = 1, basicSlash = 2, ultSlash = 3, dash = 4, jump = 5;
    [SerializeField] Collider2D slashCol, ultCol;
    [SerializeField] ParticleSystem dashPtclSys;
    [SerializeField] circleSlashAnimation ultimateAnimScr;
    Vector3 dashVect;
    int slashTmr, mobilityTmr, ultTmr;
    int basicCD, mobilityCD, ultimateCD, specialCD;
    public int recoilCD;
    bool groundedCheck;

    Vector3 vect3a, vect3b;

    private void Start()
    {
        //Time.timeScale = .1f;
        _Start(playerID, spd, 3.6f, jumpPwr, 0);
        jumpKey = KeyCode.Space; upKey = KeyCode.W; downKey = KeyCode.S; leftKey = KeyCode.A; rightKey = KeyCode.D;
        basicKey = KeyCode.U; mobilityKey = KeyCode.I; ultimateKey = KeyCode.O; specialKey = KeyCode.P;
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey) || (Input.GetKeyDown(upKey) && sameJumpAndUp)) Jump();
        if (Input.GetKeyDown(leftKey) && !Input.GetKey(rightKey) && !IsKnocked())
        {
            FaceDir(leftFace);
            SetAnimation(walking);
        }
        if (Input.GetKeyDown(rightKey) && !Input.GetKey(leftKey) && !IsKnocked())
        {
            FaceDir(rightFace);
            SetAnimation(walking);
        }    
    }
    private void FixedUpdate()
    {
        _FixedUpdate();

        if (groundedCheck != isOnGround)
        {
            groundedCheck = isOnGround;
            if (!isOnGround)
            {
                OverrideAnimation(jump);
            } else
            {
                ResumeAnimation();
            }
        }

        if (mobilityTmr > 0)
        {
            mobilityTmr--;
            rb.velocity = dashVect;
            if (mobilityTmr == 0)
            {
                ZeroVelocity();
                ResumeAnimation();
                dashPtclSys.Stop();
            }
        }
        if (slashTmr > 0)
        {
            slashTmr--;
            if (slashTmr < 1)
            {
                lockFacing--;
                ResumeAnimation();
                slashCol.enabled = false;
            }
        }
        if (ultTmr > 0)
        {
            ultTmr--;
            if (ultTmr < 1)
            {
                lockFacing--;
                ResumeAnimation();
                ultCol.enabled = false;
            }
        }

        if (Input.GetKey(basicKey) && basicCD < 1) castBasic();
        if (Input.GetKey(ultimateKey)) castUltimate();

        if (!IsKnocked())
        {
            if (Input.GetKey(mobilityKey) && mobilityCD < 1) castMobility();

            if (Input.GetKey(leftKey))
            {
                if (!Input.GetKey(rightKey))
                {
                    FaceDir(leftFace);
                    SetAnimation(walking);
                    //if (isOnGround) SetVelX(-spd);
                    //else AddVelX(-xAccl);
                    AddVelX(-xAccl);
                }
            }
            else if (Input.GetKey(rightKey))
            {
                if (!Input.GetKey(leftKey))
                {
                    FaceDir(rightFace);
                    SetAnimation(walking);
                    //if (isOnGround) SetVelX(spd);
                    //else AddVelX(xAccl);
                    AddVelX(xAccl);

                }
            } else
            {
                SetVelX(0);
                SetAnimation(idle);
            }
        }

        if (basicCD > 0) basicCD--;
        if (mobilityCD > 0) mobilityCD--;
        if (ultimateCD > 0) ultimateCD--;
        if (specialCD > 0) specialCD--;
        if (recoilCD > 0) recoilCD--;
    }

    void castBasic()
    {
        if (slashTmr > 0) return;
        slashAnim.trfm.rotation = GetRelativeAimDirectionAngle();
        slashAnim.Play();
        slashCol.enabled = true;
        slashTmr = 8;
        basicCD = 20;
        lockFacing++;
        OverrideAnimation(basicSlash);
    }
    void castMobility()
    {
        SetKnocked(7);
        setNoGravity(7);
        dashVect = GetAimDirectionVector() * 30;
        rb.velocity = dashVect;
        mobilityTmr = 7;
        mobilityCD = 40;
        dashPtclSys.Play();
        OverrideAnimation(dash);
    }
    void castUltimate()
    {
        if (ultTmr > 0) return;
        PseudoJump(15);
        ultTmr = 15;
        ultimateAnimScr.play();
        ultCol.enabled = true;
        lockFacing++;
        OverrideAnimation(ultSlash);
    }




    public void Recoil(int strength)
    {
        switch (GetAimDirection())
        {
            case aimW:
                knockback(0,1,strength);
                break;
            default:
                break;
        }
    }







    int lastAim;
    const int aimW = 0, aimWD = 1, aimD = 2, aimSD = 3, aimS = 4, aimSA = 5, aimA = 6, aimWA = 7;
    private int GetAimDirection()
    {
        if (Input.GetKey(upKey) && !Input.GetKey(downKey))
        {
            if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
            {
                lastAim = aimWD;
            } else if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
            {
                lastAim = aimWA;
            } else
            {
                lastAim = aimW;
            }
        } else if (Input.GetKey(downKey) && !Input.GetKey(upKey))
        {
            if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
            {
                lastAim = aimSD;
            }
            else if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
            {
                lastAim = aimSA;
            } else
            {
                lastAim = aimS;
            }
        } else
        {
            if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
            {
                lastAim = aimD;
            }
            else if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
            {
                lastAim = aimA;
            }
            else
            {
                //either no keys or all keys held;
                if (currentFacing == leftFace)
                {
                    lastAim = aimA;
                } else
                {
                    lastAim = aimD;
                }
            }
        }

        return lastAim;
    } //returns a Aim direction based on directional input
    private Vector3 GetAimDirectionVector() //also sets lastAim
    {
        vect3a.z = 0;
        switch (GetAimDirection())
        {
            case 0:
                vect3a.x = 0;
                vect3a.y = 1;
                break;
            case 1:
                vect3a.x = .707f;
                vect3a.y = .707f;
                break;
            case 2:
                vect3a.x = 1;
                vect3a.y = 0;
                break;
            case 3:
                vect3a.x = .707f;
                vect3a.y = -.707f;
                break;
            case 4:
                vect3a.x = 0;
                vect3a.y = -1;
                break;
            case 5:
                vect3a.x = -.707f;
                vect3a.y = -.707f;
                break;
            case 6:
                vect3a.x = -1;
                vect3a.y = 0;
                break;
            case 7:
                vect3a.x = -.707f;
                vect3a.y = .707f;
                break;
        }
        return vect3a;
    }
    private Quaternion GetAimRawDirectionAngle() //also sets lastAim, also probs doesnt work
    {
        return Quaternion.Euler(0, 0, 90 - 45 * GetAimDirection());
    }
    private Quaternion GetRelativeAimDirectionAngle() //takes into account reflecting of player sprite
    {
        bool neg90 = false;
        if (GetAimDirection() > 4 || (lastAim % 4 == 0 && currentFacing == leftFace)) neg90 = true;

        if (neg90) return Quaternion.Euler(0, 0, -90 - GetAimDirection() * 45);
        return Quaternion.Euler(0, 0, 90 - GetAimDirection() * 45);
    }
    private void OverrideAnimation(int state)
    {
        overrideState = state;
        playerAnimator.SetInteger("state", state);
        playerAnimator.SetBool("lock", true);
    }
    private bool SetAnimation(int state)
    {
        if (state == defaultState) return true;
        if (overrideState == 0)
        {
            defaultState = state;
            playerAnimator.SetInteger("state", state);
            return true;
        }
        return false;
    }
    private void ResumeAnimation()
    {
        overrideState = 0;
        playerAnimator.SetInteger("state", defaultState);
        playerAnimator.SetBool("lock", false);
    }
}
