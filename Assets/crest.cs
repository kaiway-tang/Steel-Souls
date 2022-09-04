using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crest : MonoBehaviour
{
    [SerializeField] Transform[] trfm; //0: this trfm  1: defaultPtcl trfm  2: burstPtcl trfm
    [SerializeField] Vector3 move;
    [SerializeField] ParticleSystem defaultPtcl, burstPtcl;
    [SerializeField] selfDest[] ptclDestScr;
    [SerializeField] GameObject collectedText;
    [SerializeField] Sprite[] collectedTextSpr;
    int tmr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        tmr++;
        if (tmr > 0)
        {
            trfm[0].position += move;
            if (tmr > 74) tmr = -75;
        }
        else trfm[0].position -= move;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerScript>())
        {
            manager.self.crestsCollected++;
            trfm[1].parent = null;
            trfm[2].parent = null;
            defaultPtcl.Stop();
            burstPtcl.Play();
            ptclDestScr[0].enabled = true;
            ptclDestScr[1].enabled = true;
            Instantiate(collectedText, Toolbox.camTrfm.position + new Vector3(0,5,10), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
