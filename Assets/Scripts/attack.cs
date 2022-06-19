using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField] protected int entityID, damage;
    protected void _Start(int pDamage, int pEntityID = 0)
    {
        damage = pDamage;
        entityID = pEntityID;
    }
    protected void _OnTriggerEnter2D(Collider2D col)
    {
        HPEntity HPScr;
        if (HPScr = col.GetComponent<HPEntity>())
        {
            HPScr.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        _OnTriggerEnter2D(col);
    }
}
