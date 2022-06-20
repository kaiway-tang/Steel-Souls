using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSlash : attack
{
    [SerializeField] objectPooler slashPool;
    [SerializeField] Transform trfm;
    static int layerMask = 1 << 9;
    private void OnTriggerEnter2D(Collider2D col)
    {
        HPEntity HPScr = GetHPScr(col);
        RaycastHit2D hit = Physics2D.Raycast(trfm.position, HPScr.GetPos()-trfm.position, 99, layerMask);
        for (int i = 0; i < 3; i++)
        {
            slashPool.Instantiate(hit.point, trfm.rotation);
        }
        _OnTriggerEnter2D(HPScr);
    }
}
