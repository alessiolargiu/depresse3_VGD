using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Inventory
{
    public FirstPersonController player;
    private List<WeaponEquip> weapons;
    private List<ShieldEquip> shields;
    private List<PotionEquip> potions;
    private List<HelmetEquip> helmets;
    private List<ChestEquip> chests;


    public Inventory()
    {
        weapons = new List<WeaponEquip>();
        shields = new List<ShieldEquip>();
        potions = new List<PotionEquip>();
        helmets = new List<HelmetEquip>();
        chests = new List<ChestEquip>();
    }

    public List<WeaponEquip> GetWeapons()
    {
        return weapons;
    }
    public List<ShieldEquip> GetShields()
    {
        return shields;
    }
    public List<PotionEquip> GetPotions()
    {
        return potions;
    }
    public List<HelmetEquip> GetHelmets()
    {
        return helmets;
    }
    public List<ChestEquip> GetChests()
    {
        return chests;
    }

    public void AddWeapon(WeaponEquip item)
    {
        weapons.Add(item);
    }
    public void AddShield(ShieldEquip item)
    {
        shields.Add(item);
    }
    public object AddPotion(PotionEquip item)
    {
        foreach(PotionEquip potion in player.GetInventory().GetPotions())
        {
            if(potion.nomeEquip == item.nomeEquip)
            {
                potion.potionNumber++;
                return null;
            }
        }
        item.index = PotionEquip.potionIndex;
        PotionEquip.potionIndex++;
        item.potionNumber = 0;
        potions.Add(item);
        return null;
    }
    public void AddHelmet(HelmetEquip item)
    {
        helmets.Add(item);
    }
    public void AddChest(ChestEquip item)
    {
        chests.Add(item);
    }

}
