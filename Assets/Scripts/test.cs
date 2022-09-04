using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : testParent
{
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward*10);
    }
}
