using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectActivator : MonoBehaviour
{
    Transform plyrTrfm;
    [SerializeField] obj[] objects;

    public enum activeTypeEnum { leftOfZone, rightOfZone, aboveZone, belowZone }

    [System.Serializable]
    public class obj
    {
        public activeTypeEnum activeType;
        public bool isActive;
        public GameObject[] objects;
        public float pos, max, min;
        public int checkTmr;

        public void updateStatus(Transform plyrTrfm)
        {
            if (activeType == activeTypeEnum.leftOfZone)
            {
                if (plyrTrfm.position.y < max && plyrTrfm.position.y > min)
                {
                    if (isActive != plyrTrfm.position.x < pos)
                    {
                        isActive = plyrTrfm.position.x < pos;
                        for (int i = 0; i < objects.Length; i++)
                        {
                            objects[i].SetActive(isActive);
                        }
                    }
                }
            }
            else if (activeType == activeTypeEnum.rightOfZone)
            {
                if (plyrTrfm.position.y < max && plyrTrfm.position.y > min)
                {
                    if (isActive != plyrTrfm.position.x > pos)
                    {
                        isActive = plyrTrfm.position.x > pos;
                        for (int i = 0; i < objects.Length; i++)
                        {
                            objects[i].SetActive(isActive);
                        }
                    }
                }
            }
            else if (activeType == activeTypeEnum.belowZone)
            {
                if (plyrTrfm.position.x < max && plyrTrfm.position.x > min)
                {
                    if (isActive != plyrTrfm.position.y < pos)
                    {
                        isActive = plyrTrfm.position.y < pos;
                        for (int i = 0; i < objects.Length; i++)
                        {
                            objects[i].SetActive(isActive);
                        }
                    }
                }
            }
            else if (activeType == activeTypeEnum.aboveZone)
            {
                if (plyrTrfm.position.x < max && plyrTrfm.position.x > min)
                {
                    if (isActive != plyrTrfm.position.y > pos)
                    {
                        isActive = plyrTrfm.position.y > pos;
                        for (int i = 0; i < objects.Length; i++)
                        {
                            objects[i].SetActive(isActive);
                        }
                    }
                }
            }
        }
    }
    private void Start()
    {
        plyrTrfm = Toolbox.playerTrfm;
        InvokeRepeating("checkPos", 0.5f ,0.5f);
    }

    private void checkPos()
    {
        for (int i = 1; i < objects.Length; i++)
        {
            objects[i].updateStatus(plyrTrfm);
        }
    }

}
