using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] Transform camTrfm, camPoint;
    [SerializeField] Vector3 offset;
    Transform playerTrfm; Vector3 leftRotatedZero = new Vector3(0,0,360), shockRotation = Vector3.zero, vect3;
    [SerializeField] int mode;
    const int follow = 0, followX = 1;
    Vector3 move, reverse;
    int index;
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
        if (mode == follow)
        {
            move = (camPoint.position + offset - camTrfm.position) * .1f;
            camTrfm.position += move;
            processConstraints();
        }
        else
        if (mode == followX)
        {
            move.x = (camPoint.position.x + offset.x - camTrfm.position.x) * .1f;
            move.y = 0;
            camTrfm.position += move;
            processConstraints();
        }

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

    [SerializeField] DoubleVector2[] constraints;
    void processConstraints()
    {
        for (index = 0; index < constraints.Length; index++)
        {
            if (camTrfm.position.x < constraints[index].max.x && camTrfm.position.x > constraints[index].min.x) reverse.x = move.x; else reverse.x = 0;
            if (camTrfm.position.y < constraints[index].min.y && camTrfm.position.y > constraints[index].min.y) reverse.y = move.y; else reverse.y = 0;
            camTrfm.position -= reverse;
        }
    }
}
