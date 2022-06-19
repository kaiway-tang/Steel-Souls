using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSlash : attack
{
    [SerializeField] objectPooler slashPool;
    [SerializeField] Transform trfm;
    void Start()
    {
        _Start(10, 1);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        _OnTriggerEnter2D(col);
        slashPool.Instantiate(col.transform.position, trfm.rotation)
    }
}
