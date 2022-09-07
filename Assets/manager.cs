using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    [SerializeField] manager thisScr;
    public GameObject deathTxt;
    public static manager self;

    public int crestsCollected;

    private void Awake()
    {
        self = thisScr;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) SceneManager.LoadScene("Cave Final");
    }
}
