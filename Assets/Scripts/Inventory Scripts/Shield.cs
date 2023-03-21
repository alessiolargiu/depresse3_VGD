using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield
{

    public static int staticIndex;

    private int index;
    private string name;
    private Sprite sprite;
    private float shieldValue;
    private int stamina;

    public Shield(string name, Sprite sprite, float shieldValue, int stamina)
    {
        index = staticIndex;
        staticIndex++;
        this.name = name;
        this.sprite = sprite;
        this.shieldValue = shieldValue;
        this.stamina = stamina;
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

    public float GetShieldValue()
    {
        return shieldValue;
    }

    public int GetStamina()
    {
        return stamina;
    }

    public void SetName(string name) {
        this.name = name;
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public void SetShieldValue(float shieldValue)
    {
        this.shieldValue = shieldValue;
    }

    public void SetStamina(int stamina)
    {
        this.stamina = stamina;
    }

}
