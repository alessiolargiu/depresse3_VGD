using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion {

    public static int staticIndex;

    private int index;
    private string name;
    private Sprite sprite;
    private int cureValue;

    public Potion(string name, Sprite sprite, int cureValue)
    {
        index = staticIndex;
        staticIndex++;
        this.name = name;
        this.sprite = sprite;
        this.cureValue = cureValue;
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
