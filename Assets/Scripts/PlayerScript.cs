using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MobileEntity
{
    [SerializeField] bool sameJumpAndUp;
    [SerializeField] float spd, jumpPwr;
    KeyCode jumpKey, upKey, downKey, leftKey, rightKey;
    KeyCode basicKey, mobilityKey, superKey, specialKey;

    [SerializeField] Animator slashAnim;
    Vector3 dashVect;
    int mobilityTmr;
    int basicCD, mobilityCD, superCD, specialCD;

    Vector3 vect3a, vect3b;

    private void Start()
    {
        _Start(transform, GetComponent<Rigidbody2D>(), spd, jumpPwr, 1);
        jumpKey = KeyCode.Space; upKey = KeyCode.W; downKey = KeyCode.S; leftKey = KeyCode.A; rightKey = KeyCode.D;
        basicKey = KeyCode.U; mobilityKey = KeyCode.I; superKey = KeyCode.O; specialKey = KeyCode.P;
    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey) || (Input.GetKeyDown(upKey) && sameJumpAndUp))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        _FixedUpdate();

        if (mobilityTmr > 0)
        {
            mobilityTmr--;
            rb.velocity = dashVect;
            if (mobilityTmr == 0) ZeroVelocity();
        }

        if (Input.GetKey(basicKey) && basicCD < 1)
        {
            castBasic();
        }

        if (!IsKnocked())
        {
            if (Input.GetKey(mobilityKey) && mobilityCD < 1)
            {
                castMobiltiy();
            }

            if (Input.GetKey(leftKey))
            {
                if (!Input.GetKey(rightKey))
                {
                    FaceDir(leftDir);
                    SetVelX(-spd);
                }
            }
            else if (Input.GetKey(rightKey))
            {
                if (!Input.GetKey(leftKey))
                {
                    FaceDir(rightDir);
                    SetVelX(spd);
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
    }

    void castBasic()
    {
        SetKnocked(8);
        slashAnim.Play();
        basicCD = 25;
    }
    void castMobiltiy()
    {
        SetKnocked(7);
        setNoGravity(7);
        dashVect = GetCardinalDirectionVector() * 30;
        rb.velocity = dashVect;
        mobilityTmr = 7;
        mobilityCD = 40;
    }

    int lastCardinalDir;
    const int cardinalN = 0, cardinalNE = 1, cardinalE = 2, cardinalSE = 3, cardinalS = 4, cardinalSW = 5, cardinalW = 6, cardinalNW = 7;
    protected int GetCardinalDirection()
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
                if (currentFacing == leftDir)
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
    protected Vector3 GetCardinalDirectionVector() //also sets lastCardinalDirection
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
        Debug.Log("vect3a: "+ vect3a);
        return vect3a;
    }


}
