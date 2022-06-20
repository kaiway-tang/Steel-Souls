using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    [SerializeField] int maxHP, hp;
    [SerializeField] protected Transform trfm;

    protected int entityID;
    public static int undefinedID = 0, playerID = 1;

    private void Start() { _Start(); }
    protected void _Start(int pEntityID = 0) { entityID = pEntityID; hp = maxHP; }
    public bool TakeDamage(int amount, int ignoreID = -1) //return true if entity is killed
    {
        if (ignoreID == entityID) return false;
        hp -= amount;
        if (hp <= 0)
        {
            Die();
            return true;
        }
        return false;
    }
    public Vector3 GetPos()
    {
        return trfm.position;
    }
    public bool Heal(int amount) //return true if entity is now full hp
    {
        hp += amount;
        if (hp >= maxHP)
        {
            hp = maxHP;
            return true;
        }
        return false;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public int GetHP()
    {
        return hp;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
}
