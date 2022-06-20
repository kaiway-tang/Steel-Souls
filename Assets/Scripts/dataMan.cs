using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataMan : MonoBehaviour
{
    [SerializeField] Transform s_PlayerTrfm;
    public static Transform playerTrfm;
    public static cameraScript camScr;
    private void Awake()
    {
        camScr = GetComponent<cameraScript>();
        playerTrfm = s_PlayerTrfm;
    }
}
