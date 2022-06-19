using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitSpark : MonoBehaviour
{
    [SerializeField] objectPooler objPoolScr;
    [SerializeField] Transform trfm;
    [SerializeField] Vector3 scale;
    int tmr, objID;
    private void Start()
    {
        trfm.Rotate(Vector3.forward * Random.Range(-45, 46));
    }
    private void FixedUpdate()
    {
        trfm.position += trfm.up * 1;
        trfm.localScale -= scale;
        tmr++;
        if (tmr > 5)
        {
            if (objID == 0)
            {
                Destroy(gameObject);
            } else
            {
                trfm.localScale = Vector3.one;
                objPoolScr.returnObj(objID);
                gameObject.SetActive(false);
            }
        }
    }
}
