using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion {

    private string name { get; set; }
    private Sprite sprite { get; set; }
    private int cureValue { get; set; }

    public Potion(string name, Sprite sprite, int cureValue)
    {
        this.name = name;
        this.sprite = sprite;
        this.cureValue = cureValue;
    }

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public int GetCureValue()
    {
        return cureValue;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public void SetCureValue(int cureValue)
    {
        this.cureValue = cureValue;
    }

}
