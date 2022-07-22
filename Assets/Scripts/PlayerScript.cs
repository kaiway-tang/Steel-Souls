using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MobileEntity
{
    [SerializeField] bool sameJumpAndUp;
    [SerializeField] float spd, jumpPwr;
    public static KeyCode jumpKey, upKey, downKey, leftKey, rightKey;
    public static KeyCode basicKey, mobilityKey, superKey, specialKey;

    [SerializeField] Animator slashAnim;
    [SerializeField] CapsuleCollider2D slashCol;
    [SerializeField] ParticleSystem dashPtclSys;
    Vector3 dashVect;
    int slashTmr, mobilityTmr;
    int basicCD, mobilityCD, superCD, specialCD;
    public int recoilCD;

    Vector3 vect3a, vect3b;

    private void Start()
    {
        _Start(playerID, spd, 3.6f, jumpPwr, 0);
        jumpKey = KeyCode.Space; upKey = KeyCode.W; downKey = KeyCode.S; leftKey = KeyCode.A; rightKey = KeyCode.D;
        basicKey = KeyCode.U; mobilityKey = KeyCode.I; superKey = KeyCode.O; specialKey = KeyCode.P;
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey) || (Input.GetKeyDown(upKey) && sameJumpAndUp)) Jump();
        if (Input.GetKeyDown(leftKey) && !IsKnocked()) FaceDir(leftFace);
        if (Input.GetKeyDown(rightKey) && !IsKnocked()) FaceDir(rightFace);
    }
    private void FixedUpdate()
    {
        _FixedUpdate();

        if (mobilityTmr > 0)
        {
            mobilityTmr--;
            rb.velocity = dashVect;
            if (mobilityTmr == 0)
            {
                ZeroVelocity();
                dashPtclSys.Stop();
            }
        }
        if (slashTmr > 0)
        {
            slashTmr--;
            if (slashTmr < 1)
            {
                slashCol.enabled = false;
            }
        }

            if (Input.GetKey(basicKey) && basicCD < 1) castBasic();

        if (!IsKnocked())
        {
            if (Input.GetKey(mobilityKey) && mobilityCD < 1) castMobiltiy();

            if (Input.GetKey(leftKey))
            {
                if (!Input.GetKey(rightKey))
                {
                    FaceDir(leftFace);
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
                    //if (isOnGround) SetVelX(spd);
                    //else AddVelX(xAccl);
                    AddVelX(xAccl);

                }
            } else
            {
                SetVelX(0);
            }
        }

        if (basicCD > 0) basicCD--;
        if (mobilityCD > 0) mobilityCD--;
        if (superCD > 0) superCD--;
        if (specialCD > 0) specialCD--;
        if (recoilCD > 0) recoilCD--;
    }

    void castBasic()
    {
        slashAnim.trfm.rotation = GetRelativeCardinalDirectionAngle();
        slashAnim.Play();
        slashCol.enabled = true;
        slashTmr = 8;
        basicCD = 20;
    }
    void castMobiltiy()
    {
        SetKnocked(7);
        setNoGravity(7);
        dashVect = GetCardinalDirectionVector() * 30;
        rb.velocity = dashVect;
        mobilityTmr = 7;
        mobilityCD = 40;
        dashPtclSys.Play();
    }

    int lastCardinalDir;
    const int cardinalN = 0, cardinalNE = 1, cardinalE = 2, cardinalSE = 3, cardinalS = 4, cardinalSW = 5, cardinalW = 6, cardinalNW = 7;
    private int GetCardinalDirection()
    {
        if (Input.GetKey(upKey) && !Input.GetKey(downKey))
        {
            if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
            {
                lastCardinalDir = cardinalNE;
            } else if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
            {
                lastCardinalDir = cardinalNW;
            } else
            {
                lastCardinalDir = cardinalN;
            }
        } else if (Input.GetKey(downKey) && !Input.GetKey(upKey))
        {
            if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
            {
                lastCardinalDir = cardinalSE;
            }
            else if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
            {
                lastCardinalDir = cardinalSW;
            } else
            {
                lastCardinalDir = cardinalS;
            }
        } else
        {
            if (Input.GetKey(rightKey) && !Input.GetKey(leftKey))
            {
                lastCardinalDir = cardinalE;
            }
            else if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
            {
                lastCardinalDir = cardinalW;
            }
            else
            {
                //either no keys or all keys held;
                if (currentFacing == leftFace)
                {
                    lastCardinalDir = cardinalW;
                } else
                {
                    lastCardinalDir = cardinalE;
                }
            }
        }

        return lastCardinalDir;
    } //returns a cardinal direction based on directional input
    private Vector3 GetCardinalDirectionVector() //also sets lastCardinalDirection
    {
        vect3a.z = 0;
        switch (GetCardinalDirection())
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
    private Quaternion GetCardinalRawDirectionAngle() //also sets lastCardinalDirection, also probs doesnt work
    {
        return Quaternion.Euler(0, 0, 90 - 45 * GetCardinalDirection());
    }
    private Quaternion GetRelativeCardinalDirectionAngle() //takes into account reflecting of player sprite
    {
        bool neg90 = false;
        if (GetCardinalDirection() > 4 || (lastCardinalDir % 4 == 0 && currentFacing == leftFace)) neg90 = true;

        if (neg90) return Quaternion.Euler(0, 0, -90 - GetCardinalDirection() * 45);
        return Quaternion.Euler(0, 0, 90 - GetCardinalDirection() * 45);
    }

}
