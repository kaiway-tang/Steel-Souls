using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechDrone : enemy
{
    int cd;
    bool every2;
    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (every2) everyTwo();
        every2 = !every2;
    }
    void everyTwo()
    {

    }
}
