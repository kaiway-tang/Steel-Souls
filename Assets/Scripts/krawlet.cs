using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class krawlet : patroller
{
    [SerializeField] Vector3 stretchRate;
    bool stretchIncr, every2;
    private new void Start()
    {
        _Start();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        if (every2) everyTwo();
        every2 = !every2;
    }
    void everyTwo()
    {
        if (stretchIncr)
        {
            if (trfm.localScale.x > 1) stretchIncr = false;
            trfm.localScale += stretchRate;
        } else
        {
            trfm.localScale -= stretchRate;
            if (trfm.localScale.x < .6f) stretchIncr = true;
        }
    }
}
