using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Color col;
    bool transition;
    private void FixedUpdate()
    {
        if (transition)
        {
            if (rend.color.a > 1) SceneManager.LoadScene("Jason Cave");
            rend.color += col;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerScript>())
        {
            transition = true;
        }
    }
}
