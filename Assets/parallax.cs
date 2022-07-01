using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] Transform[] backgrounds;
    [SerializeField] float[] ratios;
    [SerializeField] float rate;
    Vector2 initPos, setPos;
    Transform camTrfm;
    void Start()
    {
        camTrfm = Toolbox.camTrfm;
        initPos = camTrfm.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            setPos.x =camTrfm.position.x + (initPos.x - camTrfm.position.x) * ratios[i] * rate;
            setPos.y = backgrounds[i].position.y;
            backgrounds[i].position = setPos;
        }
    }
}
