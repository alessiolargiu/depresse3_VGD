using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor {

    public static int staticIndex;

    private int index;
    private string name;
    private Sprite sprite;
    private int stamina;
    private float armorValue;

    public Armor(string name, Sprite sprite, int stamina, float armorValue)
    {
        index = staticIndex;
        staticIndex++;
        this.name = name;
        this.sprite = sprite;
        this.stamina = stamina;
        this.armorValue = armorValue;
    }

    public int GetIndex()
    {
        return index;
    }

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public int GetStamina()
    {
        return stamina;
    }

    public float GetArmorValue()
    {
        return armorValue;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public void SetStamina(int stamina)
    {
        this.stamina = stamina;
    }

    public void SetArmorValue(float armorValue)
    {
        this.armorValue = armorValue;
    }

}
