using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInfo : MonoBehaviour
{

    private string name;
    private int index;
    public ChestEquip chestEquip;

    private void Start()
    {
        Armor chest = chestEquip.GetChest();
        name = chest.GetName();
        index = chest.GetIndex(Armor.ArmorType.Helmet);
    }

}
