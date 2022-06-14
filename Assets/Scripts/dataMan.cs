using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataMan : MonoBehaviour
{
    [SerializeField] Transform s_PlayerTrfm;
    public static Transform playerTrfm;
    private void Awake()
    {
        playerTrfm = s_PlayerTrfm;
    }
}
