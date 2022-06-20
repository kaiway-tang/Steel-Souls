using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deparent : MonoBehaviour
{
    [SerializeField] Transform[] trfms;
    void Start()
    {
        for (int i = 0; i < trfms.Length; i++)
        {
            trfms[i].parent = null;
        }
        Destroy(this);
    }
}
