using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MobileEntity
{
    Transform plyrTrfm;
    int terrainLayerMask = 1 << 6;
    RaycastHit2D hit;
    private void Start()
    {
        plyrTrfm = Toolbox.playerTrfm;
    }

    protected bool plyrInSight()
    {
        hit = Physics2D.Linecast(trfm.position, plyrTrfm.position, terrainLayerMask);
        return !hit;
    }

}
