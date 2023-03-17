using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon
{
    public static int staticIndex;

    private int index;
    private int damage;
    private int stamina;
    private string name;
    private Sprite sprite;

    public Weapon(int damage, int stamina, string name, Sprite sprite) {
        index = staticIndex;
        staticIndex++;
        this.damage = damage;
        this.stamina = stamina;
        this.name = name;
        this.sprite = sprite;
    }

    public int GetIndex()
    {
        return index;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetStamina()
    {
        return stamina;
    }

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetStamina(int stamina)
    {
        this.stamina = stamina;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

}
