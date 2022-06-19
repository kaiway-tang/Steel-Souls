using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField] int entityID, damage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        HPEntity HPScr;
        if (HPScr = col.GetComponent<HPEntity>())
        {
            HPScr.TakeDamage(damage);
        }
    }
}
