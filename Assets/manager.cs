using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    [SerializeField] manager thisScr;
    public static manager self;

    public int crestsCollected;

    private void Awake()
    {
        self = thisScr;
    }
}
