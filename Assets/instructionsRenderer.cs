using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionsRenderer : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float[] xCoords;
    [SerializeField] Color alphaChange;
    int fadingState, trackingIndex, qued = -1;
    const int none = 0, fadingIn = 1, fadingOut = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if (qued != -1 && fadingState == none)
        {
            fadingState = fadingIn;
            rend.sprite = sprites[qued];
            qued = -1;
        }

        if (Toolbox.playerTrfm.position.x > xCoords[trackingIndex])
        {
            qued = trackingIndex;
            fadingState = fadingOut;
            trackingIndex++;
        }


        if (fadingState == fadingIn)
        {
            if (rend.color.a < 1)
            {
                rend.color += alphaChange;
            }
            else
            {
                rend.color = Color.white;
                fadingState = none;
            }
        } else if (fadingState == fadingOut)
        {
            if (rend.color.a > 0)
            {
                rend.color -= alphaChange;
            }
            else
            {
                rend.color = new Color(1, 1, 1, 0);
                fadingState = none;
            }
        }
    }
}
