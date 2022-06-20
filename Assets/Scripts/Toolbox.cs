using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    public static void FaceObj(Transform trfm, Vector2 target)
    {
        trfm.rotation = Quaternion.AngleAxis(Mathf.Atan2(trfm.position.y - target.y, trfm.position.x - target.x) * Mathf.Rad2Deg + 180, Vector3.forward);
    }
}
