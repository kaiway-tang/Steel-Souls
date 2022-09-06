using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpBar : MonoBehaviour
{
    [SerializeField] Transform hpMask;
    [SerializeField] float max, diff; //diff = difference between max and min
    [SerializeField] Vector3 pos;
    float targetX;
    bool lerp;

    public void setHPPercent(float percent) //between 0 and 1
    {
        pos.x = max - diff * (1-percent);
        hpMask.localPosition = pos;
    }

    public void lerpHPPercent(float percent)
    {
        targetX = max - diff * (1-percent);
        lerp = true;
    }

    private void FixedUpdate()
    {
        if (lerp)
        {
            pos.x += (targetX - hpMask.localPosition.x)*.2f;
            hpMask.localPosition = pos;
            if (Mathf.Abs(pos.x - targetX) < diff * .005f)
            {
                pos.x = targetX;
                hpMask.localPosition = pos;
                lerp = false;
            }
        }
    }
}
