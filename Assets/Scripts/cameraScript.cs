using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] Transform camTrfm;
    Transform playerTrfm; Vector3 zOffset = new Vector3(0,0,10);
    int mode;
    const int follow = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerTrfm = dataMan.playerTrfm;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == follow)
        {
            camTrfm.position += ((playerTrfm.position-zOffset) - camTrfm.position) / 10f;
        }
    }
}
