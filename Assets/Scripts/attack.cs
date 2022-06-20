using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField] protected int entityID, damage;
    protected void _OnTriggerEnter2D(Collider2D col)
    {
        HPEntity HPScr;
        if (HPScr = col.GetComponent<HPEntity>())
        {
            HPScr.TakeDamage(damage);
        }
    }
    protected void _OnTriggerEnter2D(HPEntity HPScr)
    {
        HPScr.TakeDamage(damage);
    }
    protected HPEntity GetHPScr(Collider2D col)
    {
        HPEntity HPScr;
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
}
