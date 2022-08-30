using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectActivator : MonoBehaviour
{
    Transform plyrTrfm = Toolbox.playerTrfm;
    [SerializeField] obj[] objects;

    [System.Serializable]
    public class obj
    {
        enum activeTypeEnum { leftOfZone, rightOfZone, aboveZone, belowZone}
        activeTypeEnum activeType;
        bool isActive;
        GameObject[] objects;
        public float pos, max, min;


        void updateStatus(Transform plyrTrfm)
        {
            if (activeType == activeTypeEnum.leftOfZone)
            {
                if (plyrTrfm.position.y < max && plyrTrfm.position.y > min)
                {
                    //if (plyrTrfm.position.x < pos)
                }
            }
        }
    }
    private void Start()
    {
        InvokeRepeating("checkPos", 0.5f ,0.5f);
    }

    private void checkPos()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            
        }
    }
}
