using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Inventory
{

    private List<WeaponEquip> weapons;
    private List<ShieldEquip> shields;
    private List<PotionEquip> potions;
    private List<HelmetEquip> helmets;
    private List<ChestEquip> chests;
    private List<ShoesEquip> shoes;


    public Inventory()
    {
        weapons = new List<WeaponEquip>();
        shields = new List<ShieldEquip>();
        potions = new List<PotionEquip>();
        helmets = new List<HelmetEquip>();
        chests = new List<ChestEquip>();
        shoes = new List<ShoesEquip>();
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
    public List<ShoesEquip> GetShoes()
    {
        return shoes;
    }

    public void AddWeapon(WeaponEquip item)
    {
        weapons.Add(item);
    }
    public void AddShield(ShieldEquip item)
    {
        shields.Add(item);
    }
    public void AddPotion(PotionEquip item)
    {
        potions.Add(item);
    }
    public void AddHelmet(HelmetEquip item)
    {
        helmets.Add(item);
    }
    public void AddChest(ChestEquip item)
    {
        chests.Add(item);
    }
    public void AddShoe(ShoesEquip item)
    {
        shoes.Add(item);
    }

}
