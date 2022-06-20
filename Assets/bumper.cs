using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumper : MonoBehaviour
{
    [SerializeField] patroller patrollerScr;
    private void OnTriggerEnter2D(Collider2D col)
    {
        patrollerScr.bumped();
    }
}
