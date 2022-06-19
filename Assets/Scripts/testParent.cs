using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testParent : testParentParent
{
    public new void me()
    {
        Debug.Log("parent");
        base.me();
    }
}
