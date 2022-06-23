using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitSpark : pooledObject
{
    [SerializeField] Vector3 scale;
    int tmr;

    private void OnEnable()
    {
        tmr = 0;
        trfm.localScale = Vector3.one;
        Toolbox.FaceObj(trfm, Toolbox.playerTrfm.position);
        trfm.Rotate(Vector3.forward * Random.Range(150,210));
    }
    private void FixedUpdate()
    {
        trfm.position += trfm.right * 1;
        trfm.localScale -= scale;
        tmr++;
        if (tmr > 5)
        {
            if (objID == 0)
            {
                Destroy(gameObject);
            } else
            {
                Repool();
            }
        }
    }
}
