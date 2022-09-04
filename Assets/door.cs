using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] Transform[] trfm; //0: self  1: player  2: door left  3: door right
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite unlocked;
    [SerializeField] Vector3 move;
    [SerializeField] Collider2D col;
    [SerializeField] GameObject[] crestSlots;
    int unlockTmr = 176, crestsCollected = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (unlockTmr == 176)
        {
            if (crestsCollected != manager.self.crestsCollected)
            {
                crestSlots[crestsCollected].SetActive(true);
                crestsCollected = manager.self.crestsCollected;
            }
            if (manager.self.crestsCollected > 2)
            {
                if (trfm[0].position.x - trfm[1].position.x < 8)
                {
                    unlockTmr = 175;
                    rend.sprite = unlocked;
                }
            }
        } else if (unlockTmr > -80)
        {
            unlockTmr--;
            if (unlockTmr < 151)
            {
                if (move.x < .02f) move.x += move.x * .05f;
                trfm[2].position -= move;
                trfm[3].position += move;
            }
            if (unlockTmr < -60) col.enabled = false;
        }
    }
}
