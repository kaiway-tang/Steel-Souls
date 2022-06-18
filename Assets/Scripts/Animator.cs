using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite[] sprites;
    [SerializeField] int[] times;
    [SerializeField] bool destroyOnFinish, stop;
    int timer, index = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stop)
        {
            timer++;
            if (timer == times[index])
            {
                if (index == sprites.Length)
                {
                    if (destroyOnFinish)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        rend.sprite = null;
                        stop = true;
                    }
                }
                else
                {
                    rend.sprite = sprites[index];
                    index++;
                }
            }
        }
    }

    public void Play()
    {
        index = 1;
        timer = 0;
        stop = false;
        rend.sprite = sprites[0];
    }
}
