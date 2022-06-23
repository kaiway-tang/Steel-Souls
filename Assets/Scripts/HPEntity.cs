using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    [SerializeField] int maxHP, hp;
    [SerializeField] protected Transform trfm;
    [SerializeField] bool isHighestParent;

    protected int entityID;
    public static int undefinedID = 0, playerID = 1;

    private void Start() { _Start(); }
    protected void _Start(int pEntityID = 0) { entityID = pEntityID; hp = maxHP; }
    public bool TakeDamage(int amount, int ignoreID = -1) //return true if entity is still alive
    {
        if (ignoreID == entityID) return true;
        hp -= amount;
        if (hp <= 0)
        {
            Die();
            return false;
        }
        return true;
    }
    public virtual void knockback(Vector2 source, int strength, int ignoreID = -1) { }
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
        Destroy(trfm.gameObject);
    }
    Transform GetHighestParent(Transform pTrfm)
    {
        if (pTrfm.parent)
        {
            return GetHighestParent(pTrfm.parent);
        }
        return pTrfm;
    }

    public int GetHP()
    {
        return hp;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
    public int getEntityID()
    {
        return entityID;
    }
}
