using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] Transform camTrfm, camPoint;
    Transform playerTrfm; Vector3 zOffset = new Vector3(0,0,10), leftRotatedZero = new Vector3(0,0,360), shockRotation = Vector3.zero;
    int mode;
    const int follow = 0;
    void Start()
    {
        playerTrfm = Toolbox.playerTrfm;
        camTrfm.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == follow) camTrfm.position += (camPoint.position-zOffset - camTrfm.position) * .1f;

        processTrauma();
    }

    [SerializeField] int trauma;
    [SerializeField] float screenShakeStrength, shock;
    public void AddTrauma(int amount) { trauma += amount; }
    public void SetTrauma(int amount) { if (trauma < amount) trauma = amount; }
    void processTrauma()
    {
        if (Mathf.Abs(camTrfm.localEulerAngles.z) < .04f)
        {
            camTrfm.localEulerAngles = Vector3.zero;
        }
        else if (camTrfm.localEulerAngles.z < 180)
        {
            camTrfm.localEulerAngles += (Vector3.zero - camTrfm.localEulerAngles) * .1f;
        }
        else
        {
            camTrfm.localEulerAngles += (leftRotatedZero - camTrfm.localEulerAngles) * .1f;
        }

        if (trauma > 0)
        {
            trauma--;
            if (trauma > 60)
            {
                shock = 3600 * screenShakeStrength;
            } else
            {
                shock = trauma * trauma * screenShakeStrength;
            }
            shock = Random.Range(-shock, shock);
            shockRotation.z = shock*2;
            camTrfm.localEulerAngles += shockRotation;
            camTrfm.position += new Vector3(shock, shock = Random.Range(-shock, shock), 0);
        }
    }
}
