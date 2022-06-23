using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField] protected int entityID, damage, knockback;
    [SerializeField] protected Transform sourceTrfm;
    HPEntity HPScr;
    protected void _OnTriggerEnter2D(Collider2D col)
    {
        if (HPScr = col.GetComponent<HPEntity>())
        {
            _OnTriggerEnter2D(HPScr);
        }
    }
    protected HPEntity GetHPScr(Collider2D col)
    {
        if (HPScr = col.GetComponent<HPEntity>())
        {
            return HPScr;
        }
        return null;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        _OnTriggerEnter2D(col);
    }

    protected void _OnTriggerEnter2D(HPEntity pHPScr)
    {
        if (pHPScr.TakeDamage(damage, entityID))
        {
            pHPScr.knockback(sourceTrfm.position, knockback, entityID);
        }
    }
}
