using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : testParent
{
    private void Start()
    {
        me();
    }
    public new void me()
    {
        Debug.Log("base");
        base.me();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HPEntity HPScr;
        if (HPScr = collision.GetComponent<HPEntity>())
        {
            HPScr.TakeDamage(3);
        }
    }
}
