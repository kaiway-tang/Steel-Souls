using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainDropSplash : pooledObject
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem ptclSys;
    int tmr = 0;
    private void OnEnable()
    {
        ptclSys.Play();
    }
    private void FixedUpdate()
    {
        tmr++;
        if (tmr == 70)
        {
            tmr = 0;
            Repool();
        }
    }
}
