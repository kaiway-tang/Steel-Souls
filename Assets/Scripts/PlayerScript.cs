using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] bool sameJumpAndUp;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float spd, jumpPower;
    [SerializeField] Transform trfm;
    Vector2 vect2; Vector3 vect3;
    KeyCode jumpKey;
    KeyCode upKey;
    KeyCode leftKey;
    KeyCode rightKey;

    int currentFacing;
    int knockedUp;

    private void Start()
    {
        jumpKey = KeyCode.W;
        upKey = KeyCode.W;
        leftKey = KeyCode.A;
        rightKey = KeyCode.D;

    }

    private void Update()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            setVelY(jumpPower);
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(leftKey))
        {
            if (!Input.GetKey(rightKey))
            {
                faceDir(leftDir);
                setVelX(-spd);
            }
        } else
        if (Input.GetKey(rightKey))
        {
            if (!Input.GetKey(leftKey))
            {
                faceDir(rightDir);
                setVelX(spd);
            }
        }
        else if (knockedUp<1)
        {
            //setVelX(0);
        }
    }

    void setVelX(float val)
    {
        vect2.x = val; vect2.y = rb.velocity.y;
        rb.velocity = vect2;
    }
    void setVelY(float val)
    {
        vect2.x = rb.velocity.x; vect2.y = val;
        rb.velocity = vect2;
    }


    const int leftDir = 1;
    const int rightDir = 0;
    void faceDir(int direction)
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
}
