using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] Transform camTrfm, camPoint;
    [SerializeField] Vector3 offset;
    Transform playerTrfm; Vector3 leftRotatedZero = new Vector3(0,0,360), shockRotation = Vector3.zero, vect3;
    int mode;
    const int follow = 0;
    void Start()
    {
        playerTrfm = Toolbox.playerTrfm;
        camTrfm.parent = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) trauma += 10;
        if (Input.GetKeyDown(KeyCode.Alpha2)) trauma += 20;
        if (Input.GetKeyDown(KeyCode.Alpha3)) trauma += 30;
        if (Input.GetKeyDown(KeyCode.Alpha4)) trauma += 40;
        if (Input.GetKeyDown(KeyCode.Alpha5)) trauma += 50;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == follow) camTrfm.position += (camPoint.position+offset - camTrfm.position) * .1f;

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
            vect3.x = shock; vect3.y = Random.Range(-shock, shock);
            camTrfm.position += vect3;
        }
    }
}
