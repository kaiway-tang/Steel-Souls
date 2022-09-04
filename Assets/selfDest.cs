using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDest : MonoBehaviour
{
    [SerializeField] int tmr;
    private void FixedUpdate()
    {
        if (tmr > 0) tmr--;
        else Destroy(gameObject);
    }
}
