using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{
    [SerializeField] int count;
    int index, end;
    [SerializeField] GameObject obj;
    GameObject[] objPool;
    Transform[] trfmPool;
    [SerializeField] bool[] inUse;
    Vector2 position;
    private void Start()
    {
        objPool = new GameObject[count];
        trfmPool = new Transform[count];
        inUse = new bool[count];

        for (int i = 0; i < count; i++)
        {
            trfmPool[i] = Instantiate(obj).GetComponent<pooledObject>().Instantiate(i+1, this);
            objPool[i] = trfmPool[i].gameObject;
        }
    }

    public GameObject Instantiate(float xPos, float yPos, Quaternion rotation, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            position.x = xPos; position.y = yPos;
            while (inUse[index])
            {
                index++;
                if (index == inUse.Length) index = 0;
                if (index == end)
                {
                    Debug.Log(obj + " supply exhausted");
                    return Instantiate(obj, position, rotation);
                }
            }
            end = index;

            trfmPool[index].position = position;
            trfmPool[index].rotation = rotation;
            inUse[index] = true;
            objPool[index].SetActive(true);

            index++;
            if (index == inUse.Length) index = 0;
        }

        return objPool[index];
    }

    public void returnObj(int pIndex)
    {
        inUse[pIndex-1] = false;
    }
}
