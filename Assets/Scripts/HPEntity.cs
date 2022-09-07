using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    [SerializeField] protected int maxHP, hp, screenShake;
    [SerializeField] protected Transform trfm;
    [SerializeField] bool isHighestParent, showHP, isPlayer;
    [SerializeField] protected scaler hpBarScr;
    [SerializeField] GameObject deathFX;

    protected int entityID, invulnerable;
    public static int undefinedID = 0, playerID = 1;
    public static int untagged = 0, dashTag = 1;

    private void Start() { _Start(); }
    protected void _Start(int pEntityID = 0) { entityID = pEntityID; hp = maxHP; }
    protected void _FixedUpdate()
    {
        if (invulnerable > 0) invulnerable--;
    }
    public bool TakeDamage(int amount, int ignoreID = -1, int tag = 0) //return false if entity died OR if attack did not succeed
    {
        if (ignoreID == entityID || invulnerable > 0) return false;
        if (entityID == 1)
        {
            Toolbox.camScr.AddTrauma(amount * 80 / maxHP);
            invulnerable += 50;
        }
        hp -= amount;
        if (showHP)
        {
            hpBarScr.lerpPercent(hp/(maxHP * 1f));
        }
        if (hp <= 0)
        {
            if (tag == dashTag) Toolbox.plyrScript.Heal((int)(maxHP*.1f));
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
        if (isPlayer)
        {
            PlayerScript.self.healPtclSys.Stop();
            var main = PlayerScript.self.healPtclSys.main;
            if (amount > 20) main.duration = 2;
            else main.duration = amount * .1f;
            PlayerScript.self.healPtclSys.Play();
        }
        if (hp >= maxHP)
        {
            hp = maxHP;
            if (showHP) hpBarScr.lerpPercent(hp / (maxHP * 1f));
            return true;
        }
        if (showHP) hpBarScr.lerpPercent(hp / (maxHP * 1f));

        return false;
    }

    void Die()
    {
        Toolbox.camScr.AddTrauma(screenShake);
        Instantiate(deathFX, trfm.position, trfm.rotation);
        if (!isPlayer) Destroy(trfm.gameObject);
        else
        {
            gameObject.SetActive(false);
            manager.self.deathTxt.SetActive(true);
        }
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
