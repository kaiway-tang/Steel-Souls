using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pooledObject : MonoBehaviour
{
    [SerializeField] protected objectPooler objPoolScr;
    [SerializeField] protected Transform trfm;
    public int objID;
    public Transform Instantiate(int pObjID, objectPooler pObjPoolScr)
    {
        objID = pObjID;
        objPoolScr = pObjPoolScr;
        return trfm;
    }
    protected void Repool()
    {
        objPoolScr.returnObj(objID);
        gameObject.SetActive(false);
    }
}
