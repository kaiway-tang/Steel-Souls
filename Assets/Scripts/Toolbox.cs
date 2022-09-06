using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    [SerializeField] Transform s_PlayerTrfm, s_camTrfm, s_emptyTrfm;
    public static Transform playerTrfm, camTrfm, emptyTrfm;
    [SerializeField] PlayerScript s_plyrScript;
    public static PlayerScript plyrScript;
    [SerializeField] PhysicsMaterial2D s_defaultMaterial, s_frictionMaterial;
    public static PhysicsMaterial2D defaultMaterial, frictionMaterial;
    public static cameraScript camScr;
    private void Awake()
    {
        camScr = GetComponent<cameraScript>();
        emptyTrfm = s_emptyTrfm;
        camTrfm = s_camTrfm;
        playerTrfm = s_PlayerTrfm;
        plyrScript = s_plyrScript;
        defaultMaterial = s_defaultMaterial;
        frictionMaterial = s_frictionMaterial;
    }
    public static void FaceObj(Transform trfm, Vector2 target)
    {
        trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - target.y, trfm.position.x - target.x) * Mathf.Rad2Deg + 180, Vector3.forward);
    }
    public static Vector2 directionVector(Vector2 initial, Vector2 target)
    {
        initial.x = (target.x - initial.x);
        initial.y = (target.y - initial.y);
        return initial.normalized;
    }
    public static bool inBoxRange(Vector2 pos1, Vector2 pos2, int range)
    {
        return (Mathf.Abs(pos1.x - pos2.x) < range && Mathf.Abs(pos1.y - pos2.y) < range);
    }
}
