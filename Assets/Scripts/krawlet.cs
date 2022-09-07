using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class krawlet : patroller
{
    [SerializeField] Transform spriteTrfm;
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
            if (spriteTrfm.localScale.x > 1.2) stretchIncr = false;
            spriteTrfm.localScale += stretchRate;
        } else
        {
            spriteTrfm.localScale -= stretchRate;
            if (spriteTrfm.localScale.x < 1f) stretchIncr = true;
        }
    }
}
