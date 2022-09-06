using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    [SerializeField] int maxHP, hp, screenShake;
    [SerializeField] protected Transform trfm;
    [SerializeField] bool isHighestParent;

    protected int entityID, invulnerable;
    public static int undefinedID = 0, playerID = 1;

    private void Start() { _Start(); }
    protected void _Start(int pEntityID = 0) { entityID = pEntityID; hp = maxHP; }
    protected void _FixedUpdate()
    {
        if (invulnerable > 0) invulnerable--;
    }
    public bool TakeDamage(int amount, int ignoreID = -1) //return false if entity died OR if attack did not succeed
    {
        if (ignoreID == entityID || invulnerable > 0) return false;
        if (entityID == 1)
        {
            Toolbox.camScr.AddTrauma(amount * 80 / maxHP);
            invulnerable += 50;
        }
        hp -= amount;
        if (hp <= 0)
        {
            Die();
            return false;
        }
        return true;
    }
    public virtual void knockback(float x, float y, float strength, int ignoreID = -1, int knockedDuration = -1) { }
    public Vector3 GetPos()
    {
        return trfm.position;
    }
    public float GetPosX() { return trfm.position.x; }
    public float GetPosY() { return trfm.position.y; }
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
        Toolbox.camScr.AddTrauma(screenShake);
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
