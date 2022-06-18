using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGround : MonoBehaviour
{
    [SerializeField] MobileEntity mobileEntityScr;

    private void OnTriggerEnter2D(Collider2D col)
    {
        mobileEntityScr.OnTouchedGround();
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        mobileEntityScr.OnGround();
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        mobileEntityScr.OnLeftGround();
    }
}
