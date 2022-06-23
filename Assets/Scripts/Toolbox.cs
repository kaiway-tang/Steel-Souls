using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    [SerializeField] Transform s_PlayerTrfm, s_emptyTrfm;
    public static Transform playerTrfm, emptyTrfm;
    [SerializeField] PhysicsMaterial2D s_defaultMaterial, s_frictionMaterial;
    public static PhysicsMaterial2D defaultMaterial, frictionMaterial;
    public static cameraScript camScr;
    private void Awake()
    {
        camScr = GetComponent<cameraScript>();
        emptyTrfm = s_emptyTrfm;
        playerTrfm = s_PlayerTrfm;
        defaultMaterial = s_defaultMaterial;
        frictionMaterial = s_frictionMaterial;
    }
    public static void FaceObj(Transform trfm, Vector2 target)
    {
        trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - target.y, trfm.position.x - target.x) * Mathf.Rad2Deg + 180, Vector3.forward);
    }
}
