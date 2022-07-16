using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabSpawner : MonoBehaviour
{
    [SerializeField] int rate, count;
    [SerializeField] float xMin, xMax, yMin, yMax;
    [SerializeField] objectPooler OPScr;
    [SerializeField] Transform trfm;
    int tmr;
    [SerializeField] bool relativePos;
    private void FixedUpdate()
    {
        tmr++;
        if (tmr > 8)
        {
            tmr = 0;
            OPScr.Instantiate(Random.Range(xMin, xMax) + trfm.position.x, Random.Range(yMin, yMax) + trfm.position.y, trfm.rotation);
        }
    }
}
