using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleSlashAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Color alpha;
    [SerializeField] Transform trfm;
    int timer;
    private void Start()
    {
        timer = 60;
    }
    public bool play()
    {
        if (timer > 19)
        {
            trfm.rotation = Quaternion.identity;
            timer = 0;
            setAlpha(0);
            return true;
        }
        return false;
    }
    private void FixedUpdate()
    {
        if (timer < 20)
        {
            trfm.Rotate(Vector3.forward * -50);
            if (timer < 5) setAlpha(alpha.a + .2f);
            if (timer > 9) setAlpha(alpha.a - .1f);
            timer++;
        }
    }

    void setAlpha(float val)
    {
        alpha.a = val;
        rend.color = alpha;
    }
}
