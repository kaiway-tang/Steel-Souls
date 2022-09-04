using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelley : enemy
{
    [SerializeField] Transform trfm, plyrTrfm;
    int cd;
    void FixedUpdate()
    {
        _FixedUpdate();

        if (cd > 0)
        {
            cd--;
        }
        if (Mathf.Abs(trfm.position.x - plyrTrfm.position.x) < 9 && Mathf.Abs(trfm.position.y - plyrTrfm.position.y) < 6)
        {
            RaycastHit2D hit = Physics2D.Linecast(trfm.position, plyrTrfm.position);
            if (trfm.position.x - plyrTrfm.position.x > 0) FaceDir(leftFace);
            else FaceDir(rightFace);

            if (cd < 1)
            {

            }
        }
    }
}
