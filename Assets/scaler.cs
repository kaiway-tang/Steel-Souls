using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaler : MonoBehaviour
{
    [SerializeField] Transform mask;
    [SerializeField] float max, diff; //diff = difference between max and min
    [SerializeField] Vector3 pos;
    [SerializeField] bool vertical;
    float targetX;
    bool lerp;

    public void setPercent(float percent) //between 0 and 1
    {
        if (vertical) pos.y = max - diff * (1 - percent);
        else pos.x = max - diff * (1 - percent);
        mask.localPosition = pos;
    }

    public void lerpPercent(float percent)
    {
        targetX = max - diff * (1-percent);
        lerp = true;
    }

    private void FixedUpdate()
    {
        if (lerp)
        {
            pos.x += (targetX - mask.localPosition.x)*.2f;
            mask.localPosition = pos;
            if (Mathf.Abs(pos.x - targetX) < diff * .005f)
            {
                pos.x = targetX;
                mask.localPosition = pos;
                lerp = false;
            }
        }
    }
}
