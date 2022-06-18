using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    int hp, maxHP;

    public bool TakeDamage(int amount) //return true if entity is killed
    {
        hp -= amount;
        if (hp <= 0)
        {
            Die();
            return true;
        }
        return false;
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
