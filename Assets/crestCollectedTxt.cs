using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crestCollectedTxt : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite[] sprites;
    void Start()
    {
        rend.sprite = sprites[manager.self.crestsCollected-1];
        trfm.parent = Toolbox.camTrfm;
    }
}
