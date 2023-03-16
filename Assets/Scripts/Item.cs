using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Weapon,
        Shield,
        Helmet,
        Chest,
        Shoe,
        Potion,
    }

    public ItemType itemType;
    public int amount;

}
