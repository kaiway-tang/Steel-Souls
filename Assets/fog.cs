using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fog : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Color alpha;

    private void OnTriggerEnter2D(Collider2D col)
    {
        InvokeRepeating("Dissipate", 0, .02f);
    }
    void Dissipate()
    {
        rend.color -= alpha;
        if (rend.color.a <= 0) Destroy(gameObject);
    }
}
