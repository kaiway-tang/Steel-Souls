using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collapse : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] collapse[] others;
    [SerializeField] bool entityTrigger;
    [SerializeField] Collider2D col2D;
    bool collapsed;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (entityTrigger)
        {

        } else if (col.gameObject.name == "player")
        {
            TriggerCollapse();
        }
    }
    void TriggerCollapse()
    {
        if (collapsed) return;
        col2D.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("CN", 0.1f);
        Destroy(gameObject, 2);
        collapsed = true;
    }
    void CN() //CollapseNeighbors
    {
        for (int i = 0; i < others.Length; i++)
        {
            others[i].TriggerCollapse();
        }
    }
}
