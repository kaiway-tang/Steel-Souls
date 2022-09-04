using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Color diff;
    [SerializeField] int delay;
    private void FixedUpdate()
    {
        if (delay > 0) delay--;
        else rend.color -= diff;
    }
}
