using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechDrone : enemy
{
    int cd;
    bool every2, locked, charging, checkedLastPos = true;
    Vector2 lastPlyrPos;
    [SerializeField] ParticleSystem largeFlames;
    private new void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (cd > 0)
        {
            if (charging)
            {
                rb.velocity += plyrDirectionVector();
            }
            if (cd == 240)
            {
                charging = false;
                largeFlames.Stop();
                cd -= Random.Range(0,150);
            }
            cd--;
        }

        if (every2) everyTwo();
        every2 = !every2;
    }
    void everyTwo()
    {
        if ((Toolbox.inBoxRange(trfm.position, plyrTrfm.position, 10) || locked) && plyrInSight())
        {
            if (!locked)
            {
                locked = true;
                checkedLastPos = false;
                if (!charging) cd = Random.Range(40, 60);
            } else
            {
                facePlayerLR();
                if (!charging)
                {
                    if (Toolbox.inBoxRange(trfm.position, plyrTrfm.position, 5))
                    {
                        if (Mathf.Abs(trfm.position.y - plyrTrfm.position.y) < 3)
                        {
                            rb.velocity += Vector2.up * .2f;
                        }
                        addPlyrDirectionVelocity(-.5f);
                    }
                    else if (!Toolbox.inBoxRange(trfm.position, plyrTrfm.position, 7))
                    {
                        addPlyrDirectionVelocity(.5f);
                    }
                    else if (trfm.position.y - plyrTrfm.position.y > 6) rb.velocity += Vector2.up * -.2f;
                    if (cd < 1 && Toolbox.inBoxRange(trfm.position, plyrTrfm.position, 12))
                    {
                        cd = 300;
                        largeFlames.Play();
                        rb.velocity = Vector2.zero;
                        charging = true;
                    }
                }
            }
        }
        else
        {
            if (Toolbox.inBoxRange(trfm.position, lastPlyrPos, 5))
            {
                if (Mathf.Abs(trfm.position.y - plyrTrfm.position.y) < 3)
                {
                    rb.velocity += Vector2.up * .2f;
                }
            }
            if (locked)
            {
                lastPlyrPos = plyrTrfm.position;
                locked = false;
            }
            if (!checkedLastPos && !charging)
            {
                if (Toolbox.inBoxRange(trfm.position, lastPlyrPos, 2))
                {
                    checkedLastPos = true;
                } else
                {
                    rb.velocity += Toolbox.directionVector(trfm.position, lastPlyrPos) * .2f;
                }
            }
        }
    }
}
