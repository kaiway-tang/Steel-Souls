using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MobileEntity
{
    protected Transform plyrTrfm;
    int terrainLayerMask = 1 << 6;
    RaycastHit2D hit;
    Quaternion storeRot;
    protected void Start()
    {
        plyrTrfm = Toolbox.playerTrfm;
    }

    protected bool plyrInSight()
    {
        hit = Physics2D.Linecast(trfm.position, plyrTrfm.position, terrainLayerMask);
        return !hit;
    }
    protected Vector2 plyrDirectionVector()
    {
        vect2.x = (plyrTrfm.position.x - trfm.position.x);
        vect2.y = (plyrTrfm.position.y - trfm.position.y);
        return vect2.normalized;
    }
    protected void setPlyrDirectionVelocity(float velocity) //adds velocity relative to the direction towards the player
    {
        rb.velocity = plyrDirectionVector() * velocity;
    }
    protected void addPlyrDirectionVelocity(float velocity) //adds velocity relative to the direction towards the player
    {
        rb.velocity += plyrDirectionVector() * velocity;
    }
    protected void facePlayerLR()
    {
        if (trfm.position.x - plyrTrfm.position.x > 0) FaceDir(leftFace);
        else FaceDir(rightFace);
    }

}
