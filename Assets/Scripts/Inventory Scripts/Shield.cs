using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield
{
    private string name { get; set; }
    private Sprite sprite { get; set; }
    private float shieldValue { get; set; }

    public Shield(string name, Sprite sprite, float shieldValue)
    {
        this.name = name;
        this.sprite = sprite;
        this.shieldValue = shieldValue;
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

}
