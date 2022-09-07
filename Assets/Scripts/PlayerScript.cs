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
    [SerializeField] SpriteRenderer[] rend;
    int defaultState, overrideState;
    int[] animPriorities = {0, 10, 100, 190, 130, 20, 25}, animQue = new int[5];
    const int idle = 0, walking = 1, basicSlash = 2, ultSlash = 3, dash = 4, jump = 5, djump = 6;
    int currentAnim, djumpDeque;

    int walkState;
    const int walkStopped = 0, walkRight = 1, walkLeft = 2;

    [SerializeField] Collider2D slashCol, ultCol, dashCol, hitboxCol;
    [SerializeField] ParticleSystem dashPtclSys;
    [SerializeField] circleSlashAnimation ultimateAnimScr;
    Vector3 dashVect;
    int slashTmr, mobilityTmr, ultTmr;
    int basicCD, mobilityCD, ultimateCD, specialCD;
    [SerializeField] scaler[] cdScalers;
    public int recoilCD;
    bool groundedCheck;
    public ParticleSystem healPtclSys;

    Vector3 vect3a, vect3b;

    public static PlayerScript self;

    private void Awake()
    {
        self = this;
    }

    private void Start()
    {
        //Time.timeScale = .1f;
        _Start(playerID, spd, 3.6f, jumpPwr, 1);
        jumpKey = KeyCode.Space; upKey = KeyCode.W; downKey = KeyCode.S; leftKey = KeyCode.A; rightKey = KeyCode.D;
        basicKey = KeyCode.U; mobilityKey = KeyCode.I; ultimateKey = KeyCode.O; specialKey = KeyCode.P;

        SetAnimation(idle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey) || (Input.GetKeyDown(upKey) && sameJumpAndUp))
        {
            Jump();
            if (remainingJumps == 0)
            {
                playerAnimator.Play("Base Layer.djump",0,0);
                QueAnimation(djump);
                djumpDeque = 2;
            }
        }
            if (Input.GetKeyDown(leftKey) && !Input.GetKey(rightKey) && !IsKnocked())
        {
            FaceDir(leftFace);
            QueAnimation(walking);
        }
        if (Input.GetKeyDown(rightKey) && !Input.GetKey(leftKey) && !IsKnocked())
        {
            FaceDir(rightFace);
            QueAnimation(walking);
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
                QueAnimation(jump);
            } else
            {
                DequeAnimation(jump);

            }
        }
        if (djumpDeque > 0)
        {
            djumpDeque--;
            if (djumpDeque == 0) DequeAnimation(djump);
        }

        if (mobilityTmr > 0)
        {
            mobilityTmr--;
            rb.velocity = dashVect;
            if (mobilityTmr == 0)
            {
                ZeroVelocity();
                DequeAnimation(dash);
                dashPtclSys.Stop();
                enableHitbox();
                setColor(Color.white);
                dashCol.enabled = false;
            }
        }
        if (slashTmr > 0)
        {
            slashTmr--;
            if (slashTmr == 5) slashCol.enabled = false;
            if (slashTmr < 1)
            {
                lockFacing--;
                DequeAnimation(basicSlash);
            }
        }
        if (ultTmr > 0)
        {
            ultTmr--;
            if (ultTmr < 1)
            {
                lockFacing--;
                DequeAnimation(ultSlash);
                ultCol.enabled = false;
                enableHitbox();
            }
        }

        if (Input.GetKey(basicKey) && basicCD < 1) castBasic();
        if (Input.GetKey(ultimateKey) && ultimateCD < 1) castUltimate();

        if (!IsKnocked())
        {
            if (Input.GetKey(mobilityKey) && mobilityCD < 1) castMobility();


            if (walkState == 0)
            {
                if (Input.GetKey(leftKey))
                {
                    if (!Input.GetKey(rightKey)) doWalkLeft();
                }
                else if (Input.GetKey(rightKey))
                {
                    doWalkRight();
                }
            } else
            {
                if (Input.GetKey(leftKey))
                {
                    if (Input.GetKey(rightKey)) //both left and right
                    {
                        doWalkStopped();
                    } else //just left
                    {
                        if (walkState == walkRight) doWalkLeft();
                        else
                        {
                            AddVelX(-xAccl);
                            FaceDir(leftFace);
                        }
                    }
                } else if (Input.GetKey(rightKey)) //just right
                {
                    if (walkState == walkLeft) doWalkRight();
                    else
                    {
                        AddVelX(xAccl);
                        FaceDir(rightFace);
                    }
                } else //neither left nor right
                {
                    doWalkStopped();
                }
            }
        }

        if (basicCD > 0) basicCD--;
        if (mobilityCD > 0)
        {
            mobilityCD--;
            cdScalers[0].setPercent(1f * mobilityCD / 40);
        }
        if (ultimateCD > 0)
        {
            ultimateCD--;
            cdScalers[1].setPercent(1f * ultimateCD/400);
        }
        if (specialCD > 0) specialCD--;
        if (recoilCD > 0) recoilCD--;
    }

    void doWalkRight()
    {
        FaceDir(rightFace);
        QueAnimation(walking);
        //if (isOnGround) SetVelX(spd);
        //else AddVelX(xAccl);
        AddVelX(xAccl);
        walkState = walkRight;
    }
    void doWalkLeft()
    {
        FaceDir(leftFace);
        QueAnimation(walking);
        //if (isOnGround) SetVelX(spd);
        //else AddVelX(xAccl);
        AddVelX(-xAccl);
        walkState = walkLeft;
    }
    void doWalkStopped()
    {
        walkState = walkStopped;
        SetVelX(0);
        DequeAnimation(walking);
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
        QueAnimation(basicSlash);
    }
    void castMobility()
    {
        SetKnocked(7);
        setNoGravity(7);
        dashVect = GetAimDirectionVector() * 35;
        rb.velocity = dashVect;
        mobilityTmr = 7;
        mobilityCD = 40;
        dashPtclSys.Play();
        QueAnimation(dash);
        disableHitbox();
        setColor(Color.black);
        dashCol.enabled = true;
    }
    void castUltimate()
    {
        if (ultTmr > 0) return;
        PseudoJump(15);
        ultTmr = 15;
        ultimateCD = 400;
        ultimateAnimScr.play();
        ultCol.enabled = true;
        lockFacing++;
        QueAnimation(ultSlash);
        disableHitbox();
    }




    public void Recoil(int strength)
    {
        switch (GetAimDirection())
        {
            case aimS:
                knockback(0, 1, strength, 0, 9);
                remainingJumps = 1;
                break;
            case aimSD:
                knockback(-1, 1, strength * 1.2f, 0, 9);
                remainingJumps = 1;
                break;
            case aimSA:
                knockback(1, 1, strength * 1.2f, 0, 9);
                remainingJumps = 1;
                break;
            default:
                break;
        }
    }

    int hitboxDisable;
    void disableHitbox()
    {
        hitboxDisable++;
        hitboxCol.enabled = false;
    }

    void enableHitbox()
    {
        hitboxDisable--;
        if (hitboxDisable < 1) hitboxCol.enabled = true;
    }










    void setColor(Color col)
    {
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].color = col;
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

    private void SetAnimation(int state)
    {
        currentAnim = state;
        playerAnimator.SetInteger("state", state);
    }
    private bool QueAnimation(int state)
    {
        int i;
        for (i = 0; i < animQue.Length; i++)
            if (animQue[i] == state) return false;

        for (i = 0; i < animQue.Length; i++)
        {
            if (animQue[i] == 0)
            {
                animQue[i] = state;
                break;
            }
        }

        if (animPriorities[state] > animPriorities[currentAnim]) SetAnimation(state);
        return true;
    }
    private bool DequeAnimation(int state)
    {
        bool dequed = false;
        for (int i = 0; i < animQue.Length; i++)
        {
            if (animQue[i] == state)
            {
                animQue[i] = 0;
                dequed = true;
                break;
            }
        }
        if (!dequed) return false;

        if (state == currentAnim)
        {
            int maxPriority = animQue[0];
            for (int i = 1; i < animQue.Length; i++)
            {
                if (animPriorities[animQue[i]] > animPriorities[maxPriority])
                {
                    maxPriority = animQue[i];
                }
            }
            SetAnimation(maxPriority);
        }
        return true;
    }
}
