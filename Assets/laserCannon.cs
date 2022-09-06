using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserCannon : MonoBehaviour
{
    [SerializeField] int plyrRange, cd, fireTmr;
    [SerializeField] GameObject[] laserObjs;
    [SerializeField] Transform trfm;

    [SerializeField] SpriteRenderer[] pulse;
    [SerializeField] Color change;
    bool alphaIncr;

    private void FixedUpdate()
    {
        if (cd > 0)
        {
            if (cd == 25 && Toolbox.inBoxRange(trfm.position, Toolbox.playerTrfm.position, plyrRange))
            {
                laserObjs[2].SetActive(true);
            }
            cd--;
        } else
        {
            if (Toolbox.inBoxRange(trfm.position, Toolbox.playerTrfm.position, plyrRange))
            {
                fire();
            }
            cd = 120;
        }
        if (fireTmr > 0)
        {
            fireTmr--;
            if (alphaIncr)
            {
                setPulseColor(change.a + .33f);
                if (change.a > .9f) alphaIncr = false;
            } else
            {
                setPulseColor(change.a-.33f);
                if (change.a < .1f) alphaIncr = true;
            }
            
            if (fireTmr < 1)
            {
                laserObjs[0].SetActive(false);
                laserObjs[1].SetActive(false);
                setPulseColor(0);
            }
        }
    }

    void fire()
    {
        laserObjs[2].SetActive(false);

        laserObjs[0].SetActive(true);
        laserObjs[1].SetActive(true);
        fireTmr = 50;
        setPulseColor(1);
        alphaIncr = false;
    }

    void setPulseColor(float alpha)
    {
        change.a = alpha;
        pulse[0].color = change;
        pulse[1].color = change;
    }
}
