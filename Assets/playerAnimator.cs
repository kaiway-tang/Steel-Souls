using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimator : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Sprite[] sprites; //0:idle, 1: run
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PlayerScript.rightKey) || Input.GetKeyDown(PlayerScript.leftKey)) rend.sprite = sprites[1];
        if (Input.GetKeyUp(PlayerScript.rightKey) && !Input.GetKey(PlayerScript.leftKey)) rend.sprite = sprites[0];
        if (Input.GetKeyUp(PlayerScript.leftKey) && !Input.GetKey(PlayerScript.rightKey)) rend.sprite = sprites[0];
    }
}
