using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{
    int index, end;
    GameObject obj;
    [SerializeField] GameObject[] objPool;
    [SerializeField] Transform[] trfmPool;
    [SerializeField] bool[] inUse;

    public GameObject Instantiate(Vector2 position, Quaternion rotation)
    {
        while (inUse[index])
        {
            index++;
            if (index == inUse.Length) index = 0;
            if (index == end) return Instantiate(obj, position, rotation);
        }
        end = index;

        objPool[index].SetActive(true);
        trfmPool[index].position = position;
        trfmPool[index].rotation = rotation;
        inUse[index] = true;

        index++;
        if (index == inUse.Length) index = 0;

        return objPool[index];
    }

    public void returnObj(int pIndex)
    {
        inUse[pIndex-1] = false;
    }
}
