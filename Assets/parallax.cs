using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] Transform[] backgrounds;
    [SerializeField] Vector2[] ratios;
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
            setPos.x = camTrfm.position.x + ratios[i].x * (initPos.x - camTrfm.position.x) * rate;
            setPos.y = camTrfm.position.y + ratios[i].y * (initPos.y - camTrfm.position.y) * rate;
            backgrounds[i].position = setPos;
        }
    }
}
