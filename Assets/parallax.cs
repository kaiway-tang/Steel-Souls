using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] Transform[] backgrounds;
    [SerializeField] Vector2[] ratios;
    [SerializeField] float rate;
    Vector2 camInitPos, setPos;
    Vector2[] initPos;
    Transform camTrfm;
    void Start()
    {
        camTrfm = Toolbox.camTrfm;
        camInitPos = camTrfm.position;
        initPos = new Vector2[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            initPos[i] = backgrounds[i].position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            setPos.x = initPos[i].x + ratios[i].x * (camTrfm.position.x - camInitPos.x) * rate;
            setPos.y = initPos[i].y + ratios[i].y * (camTrfm.position.y - camInitPos.y) * rate;
            backgrounds[i].position = setPos;
        }
    }
}
