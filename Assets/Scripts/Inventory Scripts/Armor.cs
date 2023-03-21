using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor {

    public enum ArmorType
    {
        Helmet,
        Chest,
        Shoe
    }

    public static int staticIndexHelmet;
    public static int staticIndexChest;
    public static int staticIndexShoe;


    private int indexHelmet;
    private int indexChest;
    private int indexShoe;
    private string name;
    private Sprite sprite;
    private int stamina;
    private float armorValue;

    public Armor(string name, Sprite sprite, int stamina, float armorValue, ArmorType type)
    {
        switch (type)
        {
            case ArmorType.Helmet:
                indexHelmet = staticIndexHelmet;
                staticIndexHelmet++;
                break;
            case ArmorType.Chest:
                indexChest = staticIndexChest;
                staticIndexChest++;
                break;
            case ArmorType.Shoe:
                indexShoe = staticIndexShoe;
                staticIndexShoe++;
                break;
        }

        this.name = name;
        this.sprite = sprite;
        this.stamina = stamina;
        this.armorValue = armorValue;
    }

    public int GetIndex(ArmorType type)
    {
        switch (type)
        {
            case ArmorType.Helmet:
                return indexHelmet;
            case ArmorType.Chest:
                return indexChest;
            case ArmorType.Shoe:
                return indexShoe;
            default:
                return -1;
        }
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
